using ChatApplication;
using ChatApplication.BLL;
using ChatApplication.DAL;
using ChatApplication.Hubs;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddPresentationLayer()
        .AddBusinessLogicLayer()
        .AddDataAccessLayer(builder.Configuration)
        .AddSignalR();
}

var app = builder.Build();
{
    app.UseHttpsRedirection();
    app.MapControllers();
    app.MapHub<ChatHub>("/chat");
    app.Run();
}