using Microsoft.AspNetCore;
using MoveBangladesh.Persistence;
using Microsoft.EntityFrameworkCore;

namespace MoveBangladesh.CustomerAPI;

public class Program
{
	public static async Task Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		builder.Configuration.AddEnvironmentVariables("API__") // Register prefixed only environment variables
			.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
			.AddJsonFile($"appsettings.{builder.Environment.EnvironmentName}.json", optional: true, reloadOnChange: true)
			.AddJsonFile($"appsettings.Local.json", optional: true, reloadOnChange: true);

		builder.Services.ConfigureServices(builder.Configuration, builder.Environment);

		var app = builder.Build();

		using (var scope = app.Services.CreateScope())
		{
			var services = scope.ServiceProvider;

			try
			{
				var identityContext = services.GetRequiredService<ApplicationDbContext>();
				identityContext.Database.Migrate();
			}
			catch (Exception ex)
			{
				var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
				logger.LogError(ex, "An error occurred while migrating or initializing the database.");
			}
		}

		app.Configure(builder.Environment);

		app.Run();
	}
}
