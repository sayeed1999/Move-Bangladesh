using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharing.Service
{
    public static class DependencyInjection
    {
        public static IServiceCollection RegisterApplicationLayer(this IServiceCollection services)
        {
            return services
                .AddScoped<ICabService, CabService>()
                .AddScoped<ICustomerService, CustomerService>()
                .AddScoped<ICustomerRatingService, CustomerRatingService>()
                .AddScoped<IDriverService, DriverService>()
                .AddScoped<IDriverRatingService, DriverRatingService>();
        }
    }
}
