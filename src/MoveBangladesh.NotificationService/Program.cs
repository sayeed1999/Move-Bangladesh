using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MoveBangladesh.Application.Abstractions;
using MoveBangladesh.Common.MessageQueues.Abstractions;
using MoveBangladesh.Infrastructure.EventBus;
using MoveBangladesh.NotificationService.BackgroundJob;
using MoveBangladesh.NotificationService.Notifier;

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
