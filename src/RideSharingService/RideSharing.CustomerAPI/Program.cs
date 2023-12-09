using Microsoft.Extensions.Configuration;
using RideSharing.Common.Middlewares;

namespace RideSharing.CustomerAPI;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Register prefixed only environment variables.
        builder.Configuration.AddEnvironmentVariables("API__");

        builder.Services.ConfigureServices(builder.Configuration, builder.Environment);
        builder.Services.AddStackExchangeRedisCache(redisOptions =>
        {
            string connection = builder.Configuration.GetConnectionString("Redis");
            redisOptions.Configuration = connection;
        });

        var app = builder.Build();

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

        app.MapGet("/", () => "RideSharing.API is running.");

        app.Run();
    }
}
