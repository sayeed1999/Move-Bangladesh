using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using RideSharing.Application.Abstractions;
using RideSharing.Infrastructure.EventBus;
using RideSharing.NotificationService.BackgroundJob;
using RideSharing.NotificationService.Notifier;

class Program
{
	static async Task Main(string[] args)
	{
		_ = Host.CreateDefaultBuilder(args)
			.ConfigureServices(services =>
			{
				services.AddSingleton<ITripEventMessageBus, TripEventMessageBus>();
				services.AddSingleton<INotifier, Notifier>();
				services.AddHostedService<TripEventConsumer>();
			})
			.RunConsoleAsync();
	}
}
