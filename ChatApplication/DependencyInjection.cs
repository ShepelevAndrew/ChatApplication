using ChatApplication.BLL.Services.Abstraction;
using ChatApplication.Hubs.Services;

namespace ChatApplication;

public static class DependencyInjection
{
    public static IServiceCollection AddPresentationLayer(this IServiceCollection services)
    {
        services.AddControllers();
        services.AddScoped<INotificationService, NotificationService>();

        return services;
    }
}