using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;

namespace MoveBangladesh.Persistence;

public static class DependencyInjection
{
	/// <summary>
	/// Call this method to register infrastructure with all its necessary dependencies including database, dbcontext, and et all.
	/// </summary>
	/// <param name="services"></param>
	/// <param name="configuration"></param>
	/// <returns></returns>
	public static IServiceCollection RegisterPersistenceLayer(this IServiceCollection services, IConfiguration configuration)
		=> services
			.RegisterPosgreSQL(configuration)
			.RegisterDbContext();

	private static IServiceCollection RegisterPosgreSQL(this IServiceCollection services, IConfiguration configuration)
	{
		services.AddDbContext<ApplicationDbContext>(options =>
			options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

		return services;
	}

	private static IServiceCollection RegisterDbContext(this IServiceCollection services)
		=> services.AddScoped<DbContext, ApplicationDbContext>();
}
