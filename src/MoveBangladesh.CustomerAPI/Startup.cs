using MoveBangladesh.Infrastructure;
using MoveBangladesh.Application;
using MoveBangladesh.Common.Constants;
using MoveBangladesh.Common.Extensions;
using MoveBangladesh.Common.Filters;
using MoveBangladesh.Common.RegisterServices;
using MoveBangladesh.Persistence;
using FluentValidation.AspNetCore;
using System.Text.Json.Serialization;
using MoveBangladesh.Common.Configurations;
using MoveBangladesh.Common.Middlewares;
using MoveBangladesh.PushService.SignalR;

namespace MoveBangladesh.CustomerAPI;

public static class Startup
{
	public static IServiceCollection ConfigureServices(this IServiceCollection services, IConfiguration Configuration, IWebHostEnvironment Environment)
	{
		// Apply adminOnly authorization filter to all endpoints with no explicit authorize attribute.
		services.AddControllers(options =>
		{
			options.Filters.Add(new IsAdminOrAuthorizeFilter(ApplicationPolicy.AdminOnly));
		})
		.AddJsonOptions(options =>
		{
			options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
			options.JsonSerializerOptions.NumberHandling = JsonNumberHandling.AllowNamedFloatingPointLiterals;
		});

		services.AddCors(options =>
		{
			var clientAppSettings = Configuration.GetSection(nameof(ClientApplication)).Get<ClientApplication>();

			ArgumentNullException.ThrowIfNull(clientAppSettings, nameof(clientAppSettings));

			options.AddPolicy("CorsPolicy",
				builder => builder
					.WithOrigins(clientAppSettings.AllowedOrigins)
					.WithMethods("GET", "POST", "PATCH", "DELETE")
					.AllowAnyHeader());
		});


		services
			.ConfigureAppSettings(Configuration)
			.ConfigureNewtonsoftJson()
			.ConfigureApiBehavior()
			.RegisterSwagger(nameof(CustomerAPI))
			.ConfigureAuthorizationServices(Configuration, Environment)
			.RegisterPersistenceLayer(Configuration)
			.RegisterInfrastructureLayer()
			.RegisterApplicationLayer()
			.RegisterPersistenceToApplication();

		services.AddFluentValidationAutoValidation();

		services.AddHttpContextAccessor();

		return services;
	}

	// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
	public static WebApplication Configure(this WebApplication app, IWebHostEnvironment environment)
	{
		// Configure the HTTP request pipeline.
		if (environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI();
		}

		// Note: - only allow cors from code, if debugging, otherwise,
		// when nginx write cors rules, it creates duplicated cors issue!
#if DEBUG
		app.UseCors("CorsPolicy");
#endif

		// Custom middlewares.
		app.UseMiddleware<ExceptionHandlingMiddleware>();
		app.UseMiddleware<CustomExceptionHandlingMiddleware>();

		app.UseHttpsRedirection();
		app.UseAuthentication();
		app.UseAuthorization();

		app.MapControllers();

		app.MapGet("/", () => "MoveBangladesh.CustomerAPI is running.");

		app.MapHub<StatusHub>(nameof(StatusHub));

		return app;
	}
}
