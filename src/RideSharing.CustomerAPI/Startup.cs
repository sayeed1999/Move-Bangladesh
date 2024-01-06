using RideSharing.Application;
using RideSharing.Common.Constants;
using RideSharing.Common.Extensions;
using RideSharing.Common.Filters;
using RideSharing.Common.RegisterServices;
using RideSharing.Infrastructure;

namespace RideSharing.CustomerAPI;

public static class Startup
{
	public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
	{
		// Apply adminOnly authorization filter to all endpoints with no explicit authorize attribute.
		services.AddControllers(options =>
		{
			options.Filters.Add(new IsAdminOrAuthorizeFilter(ApplicationPolicy.AdminOnly));
		});

		services
			.ConfigureAppSettings(configuration)
			.ConfigureNewtonsoftJson()
			.ConfigureApiBehavior()
			.RegisterSwagger(nameof(CustomerAPI))
			.ConfigureAuthorizationServices(configuration, environment)
			.RegisterApplicationLayer()
			.RegisterInfrastructureLayer(configuration)
			.RegisterInfrastructureToApplication();

		return services;
	}
}
