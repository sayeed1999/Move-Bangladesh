using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RideSharing.AuthenticationAPI;
using RideSharing.AuthenticationAPI.Seed;
using RideSharing.AuthenticationAPI.Services;
using RideSharing.Common.Configurations;
using RideSharing.Common.RegisterServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(
	options => options.UseNpgsql(
		builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.Configure<Jwt>(builder.Configuration.GetSection(nameof(Jwt)));

builder.Services.AddScoped<TokenService>();

builder.Services
	.AddIdentityApiEndpoints<IdentityUser>()
	.AddRoles<IdentityRole>()
	.AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddAuthorization();

builder.Services.RegisterSwagger(nameof(RideSharing.AuthenticationAPI));

builder.Services.AddControllers();

var app = builder.Build();

// TODO:- take migration from program start to deployment scripts!
using (var scope = app.Services.CreateScope())
{
	using (var context = scope.ServiceProvider.GetService<ApplicationDbContext>())
	{
		await context.Database.MigrateAsync();

		var services = scope.ServiceProvider;
		await SeedData.Initialize(services);
	}
}

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.MapIdentityApi<IdentityUser>();

app.MapGet("/", () => "RideSharing.AuthenticationAPI is running!");

app.MapControllers();

app.Run();
