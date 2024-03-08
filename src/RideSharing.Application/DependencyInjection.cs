using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace RideSharing.Application;

public static class DependencyInjection
{
	public static IServiceCollection RegisterApplicationLayer(this IServiceCollection services)
	{
		var assembly = typeof(DependencyInjection).Assembly;

		services.AddAutoMapper(assembly);

		services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));

		services.AddValidatorsFromAssembly(assembly);

		return services;
	}
}
