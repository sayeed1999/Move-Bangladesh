using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Npgsql;
using RideSharing.Application.Abstractions;

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
			.RegisterDapperContext();

	private static IServiceCollection RegisterPosgreSQL(this IServiceCollection services, IConfiguration configuration)
	{
		// Call UseNetTopologySuite() when building your data source:
		var dataSourceBuilder = new NpgsqlDataSourceBuilder(configuration.GetConnectionString("DefaultConnection"));
		dataSourceBuilder.UseNetTopologySuite();
		var dataSource = dataSourceBuilder.Build();

		// Then, when configuring EF Core with UseNpgsql(), call UseNetTopologySuite() there as well:
		services.AddDbContext<ApplicationDbContext>(options =>
			options.UseNpgsql(dataSource, o => o.UseNetTopologySuite()));

		return services;
	}

	private static IServiceCollection RegisterDbContext(this IServiceCollection services)
		=> services.AddScoped<DbContext, ApplicationDbContext>();

	private static IServiceCollection RegisterDapperContext(this IServiceCollection services)
		=> services.AddScoped<IDapperContext, DapperContext>();
}
