using Microsoft.AspNetCore.Http;

namespace RideSharing.Common.Extensions;

public static class HttpContextExtensions
{
    public static bool IsInRole(this HttpContext context, string role)
    {
        if (!context.Items.ContainsKey("roles"))
        {
            return false;
        }
        if (context.Items["roles"] is not List<string> roles)
        {
            return false;
        }

        return roles.Any(r => r == role);
    }

    public static bool IsAuthenticated(this HttpContext context)
    {
        if (!context.Items.ContainsKey("isAuthenticated"))
        {
            return false;
        }
        if (context.Items["isAuthenticated"] is not bool authenticated)
        {
            return false;
        }

        return authenticated;
    }

    public static string GetAuthenticationError(this HttpContext context)
    {
        if (!context.Items.ContainsKey("Auth.Error"))
        {
            return null;
        }
        return context.Items["Auth.Error"] as string;
    }

    public static string GetUserEmail(this HttpContext context)
    {
        if (!context.Items.ContainsKey("email"))
        {
            return null;
        }
        return context.Items["email"] as string;
    }
}
