using Microsoft.Extensions.DependencyInjection;
using RideSharing.PushService.Abstraction;
using RideSharing.PushService.SignalR;

namespace RideSharing.PushService;

public static class DependencyInjection
{
    public static IServiceCollection RegisterPushService(this IServiceCollection services)
    {
        services.AddSignalR();

        services.AddSingleton<IStatusHub, StatusHub>();

        return services;
    }

    // TODO: - create support for mapping endpoints from here
}