using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RideSharing.Common.Policies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using RideSharing.Common.Configurations;

namespace RideSharing.Common.RegisterServices;

public static class AuthorizationServiceExtension
{
    public static IServiceCollection ConfigureAuthorizationServices(this IServiceCollection services, IConfiguration configuration)
    {
        var host = configuration.GetSection(nameof(Keycloak))[nameof(Keycloak.Host)];
        var realm = configuration.GetSection(nameof(Keycloak))[nameof(Keycloak.Realm)];
        var authority = $"{host}/realms/{realm}";

        services
            .AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false; // Set to true for production usage
                options.SaveToken = true;
                options.Authority = authority;

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = $"{authority}/.well-known/openid-configuration",
                    ValidateAudience = true,
                    ValidAudience = "account",                    
                };
            });

        services.AddAuthorization(options =>
            {
                options.AddPolicy(ApplicationPolicy.AdminOnly, AuthorizationPolicies.AdminOnlyPolicy);
                options.AddPolicy(ApplicationPolicy.AdminOrUser, AuthorizationPolicies.AdminOrUserPolicy);
            });

        services.AddAuthorization();

        return services;
    }
}
