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
			.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>))
			//.AddScoped<ICabRepository, CabRepository>()
			//.AddScoped<ICustomerRatingRepository, CustomerRatingRepository>()
			//.AddScoped<ICustomerRepository, CustomerRepository>()
			//.AddScoped<IDriverRatingRepository, DriverRatingRepository>()
			//.AddScoped<IDriverRepository, DriverRepository>()
			//.AddScoped<IPaymentRepository, PaymentRepository>()
			//.AddScoped<ITripRequestRepository, TripRequestRepository>()
			//.AddScoped<ITripRequestLogRepository, TripRequestLogRepository>()
			//.AddScoped<ITripRepository, TripRepository>()
			//.AddScoped<ITripLogRepository, TripLogRepository>()
			.AddScoped<IUnitOfWork, UnitOfWork>();

	public static IServiceCollection RegisterEventBuses(this IServiceCollection services)
		=> services
			.AddSingleton<ITripRequestEventMessageBus, TripRequestEventMessageBus>()
			.AddSingleton<ITripEventMessageBus, TripEventMessageBus>();
}
