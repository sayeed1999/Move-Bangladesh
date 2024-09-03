using Microsoft.EntityFrameworkCore;
using RideSharing.Common.Configurations;
using RideSharing.Common.Middlewares;
using RideSharing.Infrastructure;
using RideSharing.PushService.SignalR;
using System.Text.Json.Serialization;

namespace RideSharing.CustomerAPI;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		// Register prefixed only environment variables.
		builder.Configuration.AddEnvironmentVariables("API__");

		// Override JsonSerializer settings.
		builder.Services.AddControllers()
			.AddJsonOptions(options =>
			{
				options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
				options.JsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals;
			});

		builder.Services.AddCors(options =>
		{
			var clientAppSettings = builder.Configuration.GetSection(nameof(ClientApplication)).Get<ClientApplication>();

			options.AddPolicy("CorsPolicy",
				builder => builder
					.WithOrigins(clientAppSettings.AllowedOrigins)
					.WithMethods("GET", "POST", "PATCH", "DELETE")
					.AllowAnyHeader());
		});

		builder.Services.ConfigureServices(builder.Configuration, builder.Environment);

		var app = builder.Build();

		// Apply migration on program start
		using (var scope = app.Services.CreateScope())
		{
			using (var context = scope.ServiceProvider.GetService<ApplicationDbContext>())
			{
				context.Database.Migrate();
			}
		}

		// Configure the HTTP request pipeline.
		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI();
		}

		app.UseCors("CorsPolicy");

		// Custom middlewares.
		app.UseMiddleware<ExceptionHandlingMiddleware>();
		app.UseMiddleware<CustomExceptionHandlingMiddleware>();

		app.UseHttpsRedirection();
		app.UseAuthentication();
		app.UseAuthorization();

		app.MapControllers();

		app.MapGet("/", () => "RideSharing.CustomerAPI is running.");

		app.MapHub<StatusHub>(nameof(StatusHub));

		app.Run();
	}
}
