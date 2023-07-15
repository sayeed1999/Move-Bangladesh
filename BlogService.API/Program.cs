using BlogService.API.Entities;
using BlogService.API.MessageQueues.Receiver;
using BlogService.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using RideSharing.Common.MessageQueues.Receiver;
using RideSharing.Common.Middlewares;
using Sayeed.NTier.Generic.Repository;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Add services to the container.
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

// For Entity Framework
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration["AppSettings:ConnectionStrings:ConnStr"]));

builder.Services.AddControllers().AddNewtonsoftJson(options => {
    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

// rabbitmq emitter configs

//builder.Services.AddSingleton<UserModifiedConsumer>(provider =>
//{
//    var emitter = new UserModifiedConsumer();
//    emitter.Start();
//    return emitter;
//});

var userRegisteredConsumer = new UserRegisteredConsumer();
userRegisteredConsumer.Start();

// registering services
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Disable 404 automatic response
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

var app = builder.Build();

// stopping rabbitmq instances

var lifetime = app.Services.GetRequiredService<IHostApplicationLifetime>();
lifetime.ApplicationStopping.Register(() =>
{
    userRegisteredConsumer.Stop();
});


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Custom middlewares..
app.UseMiddleware<ExceptionHandlingMiddleware>();
app.UseMiddleware<CustomExceptionHandlingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
