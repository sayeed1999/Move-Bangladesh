using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace RideSharing.Common.RegisterServices;

public static class ApiBehaviorExtension
{
    public static IServiceCollection ConfigureApiBehavior(this IServiceCollection services)
    {
        // Disable 404 automatic response

        services.Configure<ApiBehaviorOptions>(options =>
        {
            options.SuppressModelStateInvalidFilter = true;
        });

        return services;
    }
}
