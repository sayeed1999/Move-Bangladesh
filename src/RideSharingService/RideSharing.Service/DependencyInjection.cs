using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace RideSharing.Service;

/// <summary>
/// Returns a reference to this instance of IServiceCollection
/// </summary>
public static class DependencyInjection
{
    public static IServiceCollection RegisterServices(this IServiceCollection services)
    {
        services.AddAutoMapper(Assembly.GetExecutingAssembly());

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
    
        return services;
    }

    public static IServiceCollection RegisterApplicationLayer(this IServiceCollection services)
    {
        services
            .AddScoped<ICabService, CabService>()
            .AddScoped<ICustomerService, CustomerService>()
            .AddScoped<ICustomerRatingService, CustomerRatingService>()
            .AddScoped<IDriverService, DriverService>()
            .AddScoped<IDriverRatingService, DriverRatingService>()
            .AddScoped<IPaymentService, PaymentService>()
            .AddScoped<ITripService, TripService>();

        return services;
    }
}
