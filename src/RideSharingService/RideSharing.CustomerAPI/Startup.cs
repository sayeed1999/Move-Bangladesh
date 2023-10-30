using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using RideSharing.Common.Configurations;
using RideSharing.Common.Constants;
using RideSharing.Common.Filters;
using RideSharing.Common.RegisterServices;
using RideSharing.Infrastructure;
using RideSharing.Service;
using System.Security.Claims;

namespace RideSharing.CustomerAPI;

public static class Startup
{
    public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddControllers(options =>
        {
            options.Filters.Add(new IsAdminOrAuthorizeFilter(ApplicationPolicy.AdminOnly));
        });

        // Add services to the container.
        services.Configure<AppSettings>(configuration.GetSection(nameof(AppSettings)));

        // For Entity Framework
        services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration[$"{nameof(AppSettings)}:{nameof(ConnectionStrings)}:DatabaseConnectionString"]));

        // Add other services
        services
            .ConfigureNewtonsoftJson()
            .ConfigureApiBehavior()
            .RegisterSwagger(nameof(CustomerAPI))
            .ConfigureAuthorizationServices(configuration)
            // register application layers..
            .RegisterInfrastructureLayer()
            .RegisterApplicationLayer();

        return services;
    }
}
