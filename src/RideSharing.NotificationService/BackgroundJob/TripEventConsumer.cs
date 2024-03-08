using Microsoft.Extensions.Hosting;
using RideSharing.Application.Abstractions;
using RideSharing.NotificationService.Notifier;
using RideSharing.ServiceBus.Abstractions;
using System.Text.Json;

namespace RideSharing.NotificationService.BackgroundJob
{
	internal class TripEventConsumer(
		ITripEventMessageBus messageBus,
		INotifier notifier) : BackgroundService
	{
		Func<Event, Task> notificationHandler = async (Event entity) =>
		{
			await Task.Delay(100);
			var message = $"[x] Received Message: {JsonSerializer.Serialize(entity)}";
			await notifier.Notify(message);
		};

		protected override async Task ExecuteAsync(CancellationToken stoppingToken)
		{
			await messageBus.ConsumeAsync(notificationHandler);
		}
	}
}
