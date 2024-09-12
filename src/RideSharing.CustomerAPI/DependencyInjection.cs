using RideSharing.Application.Abstractions;
using RideSharing.Common.MessageQueues.Abstractions;
using RideSharing.Infrastructure.EventBus;
using RideSharing.Persistence.Repositories;
using RideSharing.Persistence.UnitOfWork;
using RideSharing.Processor;
using RideSharing.PaymentService;

namespace RideSharing.CustomerAPI;

public static class DependencyInjection
{
	public static IServiceCollection RegisterPersistenceToApplication(this IServiceCollection services)
		=> services
			.AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
			.AddHttpContextAccessor()
			.AddScoped<IUserContext, UserContext>()
			.RegisterDatabaseRepositories()
			.RegisterProcessor()
			// .RegisterPayment()
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
