using MoveBangladesh.Application.Abstractions;
using MoveBangladesh.Common.MessageQueues.Messages;
using MoveBangladesh.NotificationService.Notifier;
using System.Text.Json;

namespace MoveBangladesh.NotificationService.BackgroundJob;

public class TripEventConsumer(
	ITripEventMessageBus messageBus,
	INotifier notifier)
	: BaseEventConsumer(messageBus, notifier)
{
	Func<TripDto, Task> notificationHandler = async (TripDto entity) =>
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
