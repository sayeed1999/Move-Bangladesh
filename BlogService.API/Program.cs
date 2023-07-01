using BlogService.API.Entities;
using BlogService.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RideSharing.Common.Middlewares;
using RideSharing.Repository;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Add services to the container.
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

// For Entity Framework
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration["AppSettings:ConnectionStrings:ConnStr"]));

builder.Services.AddControllers();

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
