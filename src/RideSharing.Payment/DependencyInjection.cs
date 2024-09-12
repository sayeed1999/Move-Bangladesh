using Microsoft.Extensions.DependencyInjection;

namespace RideSharing.PaymentService;

public static class DependencyInjection
{
	public static IServiceCollection RegisterPaymentService(this IServiceCollection services)
	{
		services.AddScoped<IPaymentFactory, PaymentFactory>();
		services.AddScoped<ICashOnDeliveryService, CashOnDeliveryService>();
		services.AddScoped<IBkashService, BkashService>();
		services.AddScoped<INagadService, NagadService>();
		services.AddScoped<ICardService, CardService>();

		return services;
	}
}
