using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using RideSharing.AuthenticationAPI;
using RideSharing.Common.RegisterServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ApplicationDbContext>(
	options => options.UseNpgsql(
		builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddIdentityApiEndpoints<IdentityUser>()
	.AddEntityFrameworkStores<ApplicationDbContext>();

builder.Services.AddAuthorization();

builder.Services.RegisterSwagger(nameof(RideSharing.AuthenticationAPI));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
	using (var context = scope.ServiceProvider.GetService<ApplicationDbContext>())
	{
		context.Database.Migrate();
	}
}

if (app.Environment.IsDevelopment())
{
	app.UseSwagger();
	app.UseSwaggerUI();
}

app.MapIdentityApi<IdentityUser>();

app.MapGet("/", () => "RideSharing.AuthenticationAPI is running!");

app.Run();
