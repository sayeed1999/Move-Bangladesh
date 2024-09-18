using MoveBangladesh.Application.Abstractions;
using MoveBangladesh.Common.MessageQueues.Abstractions;
using MoveBangladesh.Infrastructure.EventBus;
using MoveBangladesh.Persistence.Repositories;
using MoveBangladesh.Persistence.UnitOfWork;
using MoveBangladesh.Processor;
using MoveBangladesh.PaymentService;

namespace MoveBangladesh.CustomerAPI;

public static class DependencyInjection
{
	public static IServiceCollection RegisterPersistenceToApplication(this IServiceCollection services)
		=> services
			.AddSingleton<IHttpContextAccessor, HttpContextAccessor>()
			.AddHttpContextAccessor()
			.AddScoped<IUserContext, UserContext>()
			.RegisterDatabaseRepositories()
			.RegisterMoveBangladeshProcessor()
			.RegisterPaymentService()
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
