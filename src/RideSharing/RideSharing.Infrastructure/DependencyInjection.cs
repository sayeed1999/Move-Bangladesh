using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sayeed.Generic.OnionArchitecture;

namespace RideSharing.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection RegisterInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
    {
        services
            .RegisterPostgreSql<ApplicationDbContext>(configuration)
            .RegisterGenericRepositoryLayer()
            .RegisterGenericLogicLayer();

        return services;
    }
}
