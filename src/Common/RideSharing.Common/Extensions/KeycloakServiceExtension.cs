using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Keycloak.AuthServices.Authentication;
using Keycloak.AuthServices.Authorization;

namespace RideSharing.Common.RegisterServices;

public static class KeycloakServiceExtension
{
    public static IServiceCollection ConfigureKeycloakAuthorizationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddKeycloakAuthentication(configuration);

        //services.AddAuthorization(o => o.AddPolicy("IsAdmin", b =>
        //{
        //    b.RequireRealmRoles("admin");
        //    b.RequireResourceRoles("r-admin"); // stands for "resource admin"                                                   // resource roles are mapped to ASP.NET Core Identity roles
        //    b.RequireRole("r-admin");
        //}));

        services.AddKeycloakAuthorization(configuration);

        return services;
    }
}
