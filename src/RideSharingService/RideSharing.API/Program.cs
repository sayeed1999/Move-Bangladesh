using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using RideSharing.API;
using RideSharing.API.MessageQueues.Actions;
using RideSharing.Common.Middlewares;
using RideSharing.Infrastructure;
using RideSharing.Service;
using RideSharing.Common.RegisterServices;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

// For Entity Framework
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration["AppSettings:ConnectionStrings:ConnStr"]));

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

//builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(TripService).GetTypeInfo().Assembly));

// registering services
builder.Services
    .RegisterServices()
    .RegisterInfrastructureLayer()
    .RegisterApplicationLayer()
    .AddScoped<Actions>() // rabbitmq actions!
    .ConfigureApiBehavior()
    .RegisterSwagger(nameof(RideSharing.API));

var app = builder.Build();

// Register RabbitMQ emiiters & consumers to application...
app.RegisterRabbitMQToApplication();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Custom middlewares..
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseMiddleware<CustomExceptionHandlingMiddleware>();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapGet("/", () => "RideSharing.API is running.");

app.Run();