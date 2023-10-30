using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;
using RideSharing.Common.Policies;

namespace RideSharing.Common.RegisterServices;

public static class KeycloakServiceExtension
{
    public static IServiceCollection ConfigureAuthorizationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddKeycloakAuthentication(configuration);

        services.AddAuthorization(options =>
        {
            options.AddPolicy(ApplicationPolicy.AdminOnly, AuthorizationPolicies.AdminOnlyPolicy);

            options.AddPolicy(ApplicationPolicy.AdminOrUser, AuthorizationPolicies.AdminOrUserPolicy);


        });

        services.AddKeycloakAuthorization(configuration);

        return services;
    }
}
