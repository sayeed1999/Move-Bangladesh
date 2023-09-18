using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json.Serialization;
using RideSharing.Common.Middlewares;
using Sayeed.Generic.OnionArchitecture.Repository;
using System.Reflection;
using MediatR;
using RideSharing.CustomerAPI;
using RideSharing.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

// For Entity Framework
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(configuration["AppSettings:ConnectionStrings:ConnStr"]));

//builder.Services.AddAuthorization(options =>
//{
//    options.AddPolicy(AuthorizationPolicies.AdminOnly,
//        policy => policy.RequireRole(
//            Roles.Admin,
//            Roles.Moderator)
//        );
//});

//builder.Services.AddMvcCore(options =>
//{
//    options.Filters.Add(new AuthorizeFilter());
//});

builder.Services.AddControllers().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(Assembly.GetExecutingAssembly()));
// registering services
builder.Services
    .AddScoped<DbContext, ApplicationDbContext>()
    .AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
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

// Disable 404 automatic response
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

//builder.Services.AddScoped<Actions>();

var app = builder.Build();



// rabbitmq emitter configs
//var userRegisteredConsumer = new UserRegisteredConsumer();
//var userModifierConsumer = new UserModifiedConsumer();

//var scope = app.Services.CreateScope();

//var actions = scope.ServiceProvider.GetRequiredService<Actions>();
//userRegisteredConsumer.Start(actions.OnUserRegistered);
//userModifierConsumer.Start(actions.OnUserModified);

//// stopping rabbitmq instances
//var lifetime = app.Services.GetRequiredService<IHostApplicationLifetime>();
//lifetime.ApplicationStopping.Register(() =>
//{
//    userRegisteredConsumer.Stop();
//    userModifierConsumer.Stop();
//    scope.Dispose();
//});




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

app.Run();