using RideSharing.Common.Middlewares;
using RideSharing.InternalAPI;

var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddEnvironmentVariables("API__");

builder.Services.ConfigureServices(builder.Configuration, builder.Environment);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseMiddleware<CustomExceptionHandlingMiddleware>();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => "RideSharing.Internal API is running!");

app.Run();
