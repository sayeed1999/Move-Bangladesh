using RideSharing.Application;
using RideSharing.Application.Abstractions;
using RideSharing.Infrastructure.Repositories;

namespace RideSharing.CustomerAPI;

public static class DependencyInjection
{
	public static IServiceCollection RegisterInfrastructureToApplication(this IServiceCollection services)
		=> services
			.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>))
			.AddScoped<ICabRepository, CabRepository>()
			.AddScoped<ICustomerRatingRepository, CustomerRatingRepository>()
			.AddScoped<ICustomerRepository, CustomerRepository>()
			.AddScoped<IDriverRatingRepository, DriverRatingRepository>()
			.AddScoped<IDriverRepository, DriverRepository>()
			.AddScoped<IPaymentRepository, PaymentRepository>()
			.AddScoped<ITripRequestRepository, TripRequestRepository>()
			.AddScoped<ITripRepository, TripRepository>();
}
