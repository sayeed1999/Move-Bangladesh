using RideSharing.Application;
using RideSharing.Common.Configurations;
using RideSharing.Common.Constants;
using RideSharing.Common.Filters;
using RideSharing.Common.RegisterServices;
using RideSharing.Infrastructure;

namespace RideSharing.CustomerAPI;

public static class Startup
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
    {
        // Register sub-sections from appsettings.json
        services.Configure<ConnectionStrings>(configuration.GetSection(nameof(ConnectionStrings)));
        services.Configure<ClientApplication>(configuration.GetSection(nameof(ClientApplication)));
        services.Configure<Keycloak>(configuration.GetSection(nameof(Keycloak)));
        services.Configure<RedisServer>(configuration.GetSection(nameof(RedisServer)));
        services.Configure<SmtpServer>(configuration.GetSection(nameof(SmtpServer)));

        // Apply adminOnly authorization filter to all endpoints with no explicit authorize attribute.
        services.AddControllers(options =>
        {
            options.Filters.Add(new IsAdminOrAuthorizeFilter(ApplicationPolicy.AdminOnly));
        });

        // Add other services
        services
            .ConfigureNewtonsoftJson()
            .ConfigureApiBehavior()
            .RegisterSwagger(nameof(CustomerAPI))
            .ConfigureAuthorizationServices(configuration, environment)
            // register project layers
            .RegisterApplicationLayer()
            .RegisterInfrastructureLayer(configuration);

        return services;
    }
}
