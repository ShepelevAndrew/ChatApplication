using ChatApplication.DAL.Persistent;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace ChatApplication.Tests.Integration;

internal class WebApplicationFactory : WebApplicationFactory<Program>
{
    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.ConfigureTestServices(services =>
        {
            services.RemoveAll(typeof(DbContextOptions<ApplicationDbContext>));

            var connectionString = GetConnectionString();
            services.AddNpgsql<ApplicationDbContext>(connectionString);

            var dbContext = CreateDbContext(services);
            dbContext.Database.EnsureDeleted();
            dbContext.Database.Migrate();
            SeedDefaultData(dbContext);
        });
    }

    private static string? GetConnectionString()
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Development.json", optional: false, reloadOnChange: true)
            .Build();

        var connectionString = configuration.GetConnectionString("DatabaseTest");
        return connectionString;
    }

    private static ApplicationDbContext CreateDbContext(IServiceCollection services)
    {
        var serviceProvider = services.BuildServiceProvider();
        var scope = serviceProvider.CreateScope();
        var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

        return dbContext;
    }

    private static void SeedDefaultData(ApplicationDbContext dbContext)
    {
        dbContext.Users.Add(DefaultUtil.User);

        dbContext.SaveChanges();
    }
}