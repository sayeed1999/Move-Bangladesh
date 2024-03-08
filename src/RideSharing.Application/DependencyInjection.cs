using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using RideSharing.ServiceBus.Abstractions;
using RideSharing.ServiceBus.RabbitMQ;

namespace RideSharing.Application;

public static class DependencyInjection
{
	public static IServiceCollection RegisterApplicationLayer(this IServiceCollection services)
	{
		var assembly = typeof(DependencyInjection).Assembly;

		services.AddAutoMapper(assembly);

		services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));

		services.AddValidatorsFromAssembly(assembly);

		services.AddScoped<IEventBus, RabbitMQEventBus>();

		return services;
	}
}
