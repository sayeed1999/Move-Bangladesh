using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace RideSharing.Application;

/// <summary>
/// Returns a reference to this instance of IServiceCollection
/// </summary>
public static class DependencyInjection
{
    private static IServiceCollection RegisterApplicationLayerDependencies(this IServiceCollection services)
    {
        var assembly = typeof(DependencyInjection).Assembly;

        services.AddAutoMapper(assembly);

        services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(assembly));

        services.AddValidatorsFromAssembly(assembly);

        return services;
    }

    public static IServiceCollection RegisterApplicationLayer(this IServiceCollection services)
    {
        services
            .RegisterApplicationLayerDependencies()
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
