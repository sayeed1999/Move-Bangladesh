using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RideSharing.Application.Abstractions;
using RideSharing.Common.MessageQueues.Abstractions;
using RideSharing.Infrastructure.EventBus;
using RideSharing.NotificationService.BackgroundJob;
using RideSharing.NotificationService.Notifier;

class Program
{
	static async Task Main(string[] args)
	{
		using var host = Host.CreateDefaultBuilder(args)
			.ConfigureServices(services =>
			{
				services
					.AddSingleton<ITripRequestEventMessageBus, TripRequestEventMessageBus>()
					.AddSingleton<ITripEventMessageBus, TripEventMessageBus>();

				services.AddSingleton<INotifier, Notifier>();

				services
					.AddHostedService<TripRequestEventConsumer>()
					.AddHostedService<TripEventConsumer>();
			})
			.Build();

		await host.RunAsync();
	}
}
