using ChatApplication;
using ChatApplication.BLL;
using ChatApplication.DAL;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddPresentationLayer()
        .AddBusinessLogicLayer()
        .AddDataAccessLayer(builder.Configuration);
}

var app = builder.Build();
{
    app.UseHttpsRedirection();
    app.MapControllers();
    app.Run();
}