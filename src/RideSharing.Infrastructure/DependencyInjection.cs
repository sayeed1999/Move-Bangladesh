using Microsoft.Extensions.DependencyInjection;
using RideSharing.Application.Abstractions;
using RideSharing.Common;

namespace RideSharing.Infrastructure;

public static class DependencyInjection
{
	public static IServiceCollection RegisterInfrastructureLayer(this IServiceCollection services)
	{
		services.AddTransient<INotificationService, NotificationService>();
		services.AddTransient<IDateTime, MachineDateTime>();

		return services;
	}
}
