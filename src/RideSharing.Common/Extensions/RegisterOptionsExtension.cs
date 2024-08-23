using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RideSharing.Common.Configurations;

namespace RideSharing.Common.Extensions;

public static class RegisterOptionsExtension
{
	public static IServiceCollection ConfigureAppSettings(this IServiceCollection services, IConfiguration configuration)
	{
		// Register sub-sections from appsettings.json

		services.Configure<Jwt>(configuration.GetSection(nameof(Jwt)));
		services.Configure<ConnectionStrings>(configuration.GetSection(nameof(ConnectionStrings)));
		services.Configure<ClientApplication>(configuration.GetSection(nameof(ClientApplication)));
		services.Configure<Keycloak>(configuration.GetSection(nameof(Keycloak)));
		services.Configure<RedisServer>(configuration.GetSection(nameof(RedisServer)));
		services.Configure<SmtpServer>(configuration.GetSection(nameof(SmtpServer)));

		return services;
	}
}
