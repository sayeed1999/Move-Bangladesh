using RideSharing.Application.Abstractions;
using RideSharing.Common.MessageQueues.Abstractions;
using RideSharing.Infrastructure.EventBus;
using RideSharing.Infrastructure.Repositories;
using RideSharing.Infrastructure.UnitOfWork;
using RideSharing.Processor;

namespace RideSharing.CustomerAPI;

public static class DependencyInjection
{
	public static IServiceCollection RegisterInfrastructureToApplication(this IServiceCollection services)
		=> services
			.AddScoped<IHttpContextAccessor, HttpContextAccessor>()
			.AddScoped<IUserContext, UserContext>()
			.RegisterDatabaseRepositories()
			.RegisterProcessor()
			.RegisterEventBuses();

	public static IServiceCollection RegisterDatabaseRepositories(this IServiceCollection services)
		=> services
			// generic repository
			.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>))
			// inherited repositories
			.AddScoped<ITripRequestRepository, TripRequestRepository>()
			.AddScoped<ITripRepository, TripRepository>()
			// unit of work
			.AddScoped<IUnitOfWork, UnitOfWork>();

	public static IServiceCollection RegisterEventBuses(this IServiceCollection services)
		=> services
			.AddSingleton<ITripRequestEventMessageBus, TripRequestEventMessageBus>()
			.AddSingleton<ITripEventMessageBus, TripEventMessageBus>();
}
