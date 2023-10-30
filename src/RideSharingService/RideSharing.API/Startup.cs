using Microsoft.EntityFrameworkCore;
using RideSharing.Common.Configurations;
using RideSharing.Common.RegisterServices;
using RideSharing.Infrastructure;
using RideSharing.Service;

namespace RideSharing.API;

public static class Startup
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        // Add services to the container.
        services.Configure<AppSettings>(configuration.GetSection(nameof(AppSettings)));

        // For Entity Framework
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration[$"{nameof(AppSettings)}:{nameof(ConnectionStrings)}:DatabaseConnectionString"]));

        // Add other services
        services
            .ConfigureNewtonsoftJson()
            .ConfigureApiBehavior()
            .RegisterSwagger(nameof(API))
            .ConfigureKeycloakAuthorizationServices(configuration)
            // register application layers..
            .RegisterInfrastructureLayer()
            .RegisterApplicationLayer();

        return services;
    }
}
