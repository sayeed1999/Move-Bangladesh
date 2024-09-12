using RideSharing.Application.Abstractions;
using RideSharing.Common.Constants;
using RideSharing.Common.Extensions;
using RideSharing.Common.Filters;
using RideSharing.Common.RegisterServices;
using RideSharing.Persistence;
using RideSharing.Persistence.Repositories;
using RideSharing.Persistence.UnitOfWork;

namespace RideSharing.InternalAPI;

public static class DependencyInjection
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
			.RegisterSwagger(nameof(InternalAPI))
			.ConfigureAuthorizationServices(configuration, environment)
			.RegisterPersistenceLayer(configuration)
			.RegisterServices();

		return services;
	}

	public static IServiceCollection RegisterServices(this IServiceCollection services)
	{
		return services
			// generic repository
			.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>))
			// inherited repositories
			.AddScoped<ITripRequestRepository, TripRequestRepository>()
			.AddScoped<ITripRepository, TripRepository>()
			// unit of work
			.AddScoped<IUnitOfWork, UnitOfWork>();
	}
}
