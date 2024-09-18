using Microsoft.Extensions.DependencyInjection;
using MoveBangladesh.Application.Abstractions;
using MoveBangladesh.Common;

namespace MoveBangladesh.Infrastructure;

public static class DependencyInjection
{
	public static IServiceCollection RegisterInfrastructureLayer(this IServiceCollection services)
	{
		services.AddTransient<INotificationService, NotificationService>();
		services.AddTransient<IDateTime, MachineDateTime>();

		return services;
	}
}
