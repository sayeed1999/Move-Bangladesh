using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using MoveBangladesh.AuthenticationAPI;
using MoveBangladesh.AuthenticationAPI.Seed;
using MoveBangladesh.AuthenticationAPI.Services;
using MoveBangladesh.Common.Configurations;
using MoveBangladesh.Common.RegisterServices;
using MoveBangladesh.Domain.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(
	options => options.UseNpgsql(
		builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.Configure<Jwt>(builder.Configuration.GetSection(nameof(Jwt)));

builder.Services.AddCors(options =>
{
	var clientAppSettings = builder.Configuration.GetSection(nameof(ClientApplication)).Get<ClientApplication>();

	options.AddPolicy("CorsPolicy",
		builder => builder
			.WithOrigins(clientAppSettings.AllowedOrigins)
			.WithMethods("GET", "POST", "PATCH", "DELETE")
			.AllowAnyHeader());
});

builder.Services.AddScoped<TokenService>();

builder.Services
	.AddIdentityApiEndpoints<User>()
	.AddRoles<IdentityRole>()
	.AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddAuthorization();

builder.Services.RegisterSwagger(nameof(MoveBangladesh.AuthenticationAPI));

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

// Note: - only allow cors from code, if debugging, otherwise,
// when nginx write cors rules, it creates duplicated cors issue!
#if DEBUG
app.UseCors("CorsPolicy");
#endif

app.MapIdentityApi<User>();

app.MapGet("/", () => "MoveBangladesh.AuthenticationAPI is running!");

app.MapControllers();

app.Run();
