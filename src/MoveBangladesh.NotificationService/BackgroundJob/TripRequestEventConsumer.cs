using MoveBangladesh.Common.MessageQueues.Abstractions;
using MoveBangladesh.Common.MessageQueues.Messages;
using MoveBangladesh.NotificationService.Notifier;
using System.Text.Json;

namespace MoveBangladesh.NotificationService.BackgroundJob;

public class TripRequestEventConsumer(
	ITripRequestEventMessageBus messageBus,
	INotifier notifier)
	: BaseEventConsumer(messageBus, notifier)
{
	Func<TripRequestDto, Task> notificationHandler = async (TripRequestDto entity) =>
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
