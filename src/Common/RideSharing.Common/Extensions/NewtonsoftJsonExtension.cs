using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;

namespace RideSharing.Common.RegisterServices;

public static class NewtonsoftJsonExtension
{
    public static IServiceCollection ConfigureNewtonsoftJson(this IServiceCollection services)
    {
        services.AddControllers().AddNewtonsoftJson(options =>
        {
            options.SerializerSettings.ContractResolver = new CamelCasePropertyNamesContractResolver();
            options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
        });

        return services;
    }
}
