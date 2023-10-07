using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Sayeed.Generic.OnionArchitecture.Repository;

namespace RideSharing.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection RegisterInfrastructureLayer(this IServiceCollection services)
    {
        services
            .AddScoped<DbContext, ApplicationDbContext>()
            .AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));


        return services;
    }
}
