using ChatApplication.BLL.Services.Abstraction;
using ChatApplication.BLL.Services.Implementation;
using Microsoft.Extensions.DependencyInjection;

namespace ChatApplication.BLL;

public static class DependencyInjection
{
    public static IServiceCollection AddBusinessLogicLayer(this IServiceCollection services)
    {
        services.AddScoped<IChatService, ChatService>();
        services.AddScoped<IUserService, UserService>();

        return services;
    }
}