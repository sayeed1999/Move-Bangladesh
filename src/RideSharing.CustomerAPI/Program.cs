using Microsoft.EntityFrameworkCore;
using RideSharing.Common.Middlewares;
using RideSharing.Infrastructure;

namespace RideSharing.CustomerAPI;

public class Program
{
	public static void Main(string[] args)
	{
		var builder = WebApplication.CreateBuilder(args);

		// Register prefixed only environment variables.
		builder.Configuration.AddEnvironmentVariables("API__");

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

		// Custom middlewares.
		app.UseMiddleware<ExceptionHandlingMiddleware>();
		app.UseMiddleware<CustomExceptionHandlingMiddleware>();

		app.UseHttpsRedirection();
		app.UseAuthentication();
		app.UseAuthorization();

		app.MapControllers();

		app.MapGet("/", () => "RideSharing.CustomerAPI is running.");

		app.Run();
	}
}
