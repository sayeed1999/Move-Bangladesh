using Microsoft.Extensions.DependencyInjection;
using MoveBangladesh.PushService.Abstraction;
using MoveBangladesh.PushService.SignalR;

namespace MoveBangladesh.PushService;

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
