using System;
using Microsoft.AspNetCore.Identity;
using MoveBangladesh.Common.Constants;

namespace MoveBangladesh.AuthenticationAPI.Seed;

public class SeedData
{
	public static async Task Initialize(IServiceProvider serviceProvider)
	{
		var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

		var roles = new[]
		{
			ApplicationRole.Admin,
			ApplicationRole.Driver,
			ApplicationRole.Customer,
		};

		foreach (var role in roles)
		{
			if (!await roleManager.RoleExistsAsync(role))
			{
				await roleManager.CreateAsync(new IdentityRole(role));
			}
		}
	}
}
