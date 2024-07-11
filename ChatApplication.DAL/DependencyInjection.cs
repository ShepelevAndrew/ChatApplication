using ChatApplication.DAL.Persistent;
using ChatApplication.DAL.Persistent.Repositories.Abstraction;
using ChatApplication.DAL.Persistent.Repositories.Implementation;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ChatApplication.DAL;

public static class DependencyInjection
{
    public static IServiceCollection AddDataAccessLayer(this IServiceCollection services, IConfiguration configuration)
    {
        var connection = configuration.GetConnectionString("Database");
        services.AddDbContext<ApplicationDbContext>(
            options => options.UseNpgsql(connection));

        services.AddScoped<IChatRepository, ChatRepository>();
        services.AddScoped<IUserRepository, UserRepository>();

        return services;
    }
}