using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace RideSharing.Common.Policies;

public static class AuthorizationPolicies
{
    /// <summary>
    /// This policy indicates the user must have role claim of 'admin'.
    /// </summary>
    public static AuthorizationPolicy AdminOnlyPolicy { get; }
        = new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .RequireClaim(ClaimTypes.Role, ApplicationRole.Admin)
            .Build();

    /// <summary>
    /// This policy indicates the user must have role claim of either of the two: 'admin', 'user'.
    /// </summary>
    public static AuthorizationPolicy AdminOrUserPolicy { get; }
        = new AuthorizationPolicyBuilder()
            .RequireAuthenticatedUser()
            .RequireClaim(ClaimTypes.Role, ApplicationRole.Admin, ApplicationRole.User)
            .Build();
}
