using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using MoveBangladesh.Common.Policies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MoveBangladesh.Common.Configurations;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace MoveBangladesh.Common.RegisterServices;

public static class AuthorizationServiceExtension
{
    public static IServiceCollection ConfigureAuthorizationServices(this IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
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
                // Valid ssl check is disabled for development environment because keycloak http port has no ssl!
                if (environment.IsDevelopment())
                {
                    options.RequireHttpsMetadata = false;
                }

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
