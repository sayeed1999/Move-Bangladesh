using MediatR;
using Microsoft.Extensions.DependencyInjection;
using RideSharing.Application.Abstractions;
using RideSharing.Application.Common.Behaviors;
using RideSharing.Application.Services;
using static RideSharing.Application.Extensions.PollyResilienceStrategy;

namespace RideSharing.Application;

public static class DependencyInjection
{
	public static IServiceCollection RegisterApplicationLayer(this IServiceCollection services)
	{
		var assembly = typeof(DependencyInjection).Assembly;

		services.AddAutoMapper(assembly);
		services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));
		services.RegisterPipelineBehaviors();
		services.RegisterHttpClients();

		return services;
	}

	public static IServiceCollection RegisterPipelineBehaviors(this IServiceCollection services)
	{
		services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestPerformanceBehaviour<,>));
		services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RequestValidationBehavior<,>));

		return services;
	}

	public static IServiceCollection RegisterHttpClients(this IServiceCollection services)
	{
		services.AddHttpClient<IRideSharingProcessor, RideSharingProcessor>(client => 
		{
			client.BaseAddress = new Uri("http://localhost:7000/api");
		})
		.SetHandlerLifetime(TimeSpan.FromMinutes(5)) // default is 2 minutes
		.AddPolicyHandler(GetRetryPolicy());

		return services;
	}
}
