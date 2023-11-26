using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;
using System.Linq;
using System.Threading.Tasks;

namespace RideSharing.Common.Filters;

public class IsAdminOrAuthorizeFilter : AuthorizeFilter
{
    public IsAdminOrAuthorizeFilter(string policyName) : base(policyName)
    {

    }

    public override Task OnAuthorizationAsync(AuthorizationFilterContext context)
    {
        var endpointHasAuthorizeAttribute = 
            context.ActionDescriptor.EndpointMetadata.Any(em => em is AuthorizeAttribute);

        // If there is another authorize filter, do nothing
        if (endpointHasAuthorizeAttribute)
        {
            return Task.FromResult(0);
        }

        //Otherwise apply this policy
        return base.OnAuthorizationAsync(context);
    }
}
