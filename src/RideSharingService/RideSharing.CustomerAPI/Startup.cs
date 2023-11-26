using Microsoft.EntityFrameworkCore;
using RideSharing.Common.Configurations;
using RideSharing.Common.Constants;
using RideSharing.Common.Filters;
using RideSharing.Common.RegisterServices;
using RideSharing.Infrastructure;
using RideSharing.Service;

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

        // For Entity Framework
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration.GetConnectionString(nameof(ConnectionStrings.DatabaseConnectionString))));

        // Add other services
        services
            .ConfigureNewtonsoftJson()
            .ConfigureApiBehavior()
            .RegisterSwagger(nameof(CustomerAPI))
            .ConfigureAuthorizationServices(configuration, environment)
            // register application layers..
            .RegisterInfrastructureLayer()
            .RegisterApplicationLayer();

        return services;
    }
}
