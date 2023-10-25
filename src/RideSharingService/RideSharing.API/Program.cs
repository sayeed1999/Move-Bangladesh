using RideSharing.Common.Middlewares;

namespace RideSharing.API;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Register prefixed only environment variables.
        builder.Configuration.AddEnvironmentVariables("AdminAPI__");

        builder.Services.ConfigureServices(builder.Configuration);

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
