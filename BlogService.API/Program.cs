using BlogService.API.Entities;
using BlogService.API.MessageQueues.Actions;
using BlogService.API.MessageQueues.Receiver;
using BlogService.Entity;
using BlogService.Infrastructure;
using BlogService.Service.CommentService;
using BlogService.Service.EdgeRepository;
using BlogService.Service.NodeRepository;
using BlogService.Service.PostService;
using BlogService.Service.UserRelationRepository;
using BlogService.Service.UserRelationService;
using BlogService.Service.UserRepository;
using BlogService.Service.UserService;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json.Serialization;
using RideSharing.Common.MessageQueues.Receiver;
using RideSharing.Common.Middlewares;
using Sayeed.Generic.OnionArchitecture.Repository;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Add services to the container.
builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

// For Entity Framework
builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer(configuration["AppSettings:ConnectionStrings:ConnStr"]), ServiceLifetime.Scoped);

// registering repository
builder.Services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserRelationRepository, UserRelationRepository>();
builder.Services.AddScoped<INodeRepository, NodeRepository>();
builder.Services.AddScoped<IEdgeRepository, EdgeRepository>();

// registering services
builder.Services.AddScoped<DbContext, AppDbContext>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IUserRelationService, UserRelationService>();
builder.Services.AddScoped<IPostService, PostService>();
builder.Services.AddScoped<ICommentService, CommentService>();



builder.Services.AddControllers().AddNewtonsoftJson(options => {
    options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
    options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
});

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddScoped<Actions>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Disable 404 automatic response
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

var app = builder.Build();


// rabbitmq emitter configs
var userRegisteredConsumer = new UserRegisteredConsumer();
var userModifierConsumer = new UserModifiedConsumer();

var scope = app.Services.CreateScope();

var actions = scope.ServiceProvider.GetRequiredService<Actions>();
userRegisteredConsumer.Start(user => actions.OnUserRegistered(user));
userModifierConsumer.Start(user => actions.OnUserModified(user));


// stopping rabbitmq instances
var lifetime = app.Services.GetRequiredService<IHostApplicationLifetime>();
lifetime.ApplicationStopping.Register(() =>
{
    userRegisteredConsumer.Stop();
    userModifierConsumer.Stop();
    scope.Dispose();
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
