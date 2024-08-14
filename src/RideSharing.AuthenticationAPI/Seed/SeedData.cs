using System;
using Microsoft.AspNetCore.Identity;
using RideSharing.Common.Constants;

namespace RideSharing.AuthenticationAPI.Seed;

public class SeedData
{
    public static async Task Initialize(IServiceProvider serviceProvider)
    {
        var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        var roles = new[] 
        { 
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
