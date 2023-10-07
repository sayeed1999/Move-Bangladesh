using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using RideSharing.API.MessageQueues.Actions;
using RideSharing.API.MessageQueues.Receiver;

namespace RideSharing.API;

public static class DependencyInjection
{
    public static IServiceCollection ConfigureApiBehavior(this IServiceCollection services)
    {
        // Disable 404 automatic response
        
        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });

        return services;
    }

    public static IServiceCollection RegisterSwagger(this IServiceCollection services)
    {
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

        services.AddEndpointsApiExplorer();
        
        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "RideSharing.API", Version = "v1" });
            c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
            {
                Name = "Authorization",
                In = ParameterLocation.Header,
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement()
            {
                {
                    new OpenApiSecurityScheme
                    {
                    Reference = new OpenApiReference
                        {
                            Type = ReferenceType.SecurityScheme,
                            Id = "Bearer"
                        },
                        Scheme = "Bearer",
                        Name = "Bearer",
                        In = ParameterLocation.Header,
                        Type = SecuritySchemeType.Http
                    },
                    new List<string>()
                }
            });
        });

        return services;
    }

    public static void RegisterRabbitMQToApplication(this WebApplication app)
    {
        // rabbitmq emitter configs
        var userRegisteredConsumer = new UserRegisteredConsumer();
        var userModifierConsumer = new UserModifiedConsumer();

        var scope = app.Services.CreateScope();

        var actions = scope.ServiceProvider.GetRequiredService<Actions>();
        userRegisteredConsumer.Start(actions.OnUserRegistered);
        userModifierConsumer.Start(actions.OnUserModified);

        // stopping rabbitmq instances
        var lifetime = app.Services.GetRequiredService<IHostApplicationLifetime>();
        lifetime.ApplicationStopping.Register(() =>
        {
            userRegisteredConsumer.Stop();
            userModifierConsumer.Stop();
            scope.Dispose();
        });
    }
}
