using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RideSharing.Application.Common.Behaviors;

namespace RideSharing.Application;

public static class DependencyInjection
{
	public static IServiceCollection RegisterApplicationLayer(this IServiceCollection services)
	{
		var assembly = typeof(DependencyInjection).Assembly;

		services.AddAutoMapper(assembly);

		services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));

		services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));

		services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

		return services;
	}
}
