using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RideSharing.Application.Abstractions;
using RideSharing.Infrastructure.Repositories;

namespace RideSharing.Infrastructure;

public static class DependencyInjection
{
    /// <summary>
    /// Call this method to register infrastructure with all its necessary dependencies including database, dbcontext, and et all.
    /// </summary>
    /// <param name="services"></param>
    /// <param name="configuration"></param>
    /// <returns></returns>
    public static IServiceCollection RegisterInfrastructureLayer(this IServiceCollection services, IConfiguration configuration)
        => services
            .RegisterPosgreSQL(configuration)
            .RegisterDbContext()
            .RegisterRepository();

    private static IServiceCollection RegisterPosgreSQL(this IServiceCollection services, IConfiguration configuration)
        => services.AddDbContext<ApplicationDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

    private static IServiceCollection RegisterDbContext(this IServiceCollection services)
        => services.AddScoped<DbContext, ApplicationDbContext>();

    private static IServiceCollection RegisterRepository(this IServiceCollection services)
        => services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
}
