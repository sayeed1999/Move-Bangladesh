using Microsoft.Extensions.Hosting;
using MoveBangladesh.NotificationService.Notifier;
using MoveBangladesh.ServiceBus.Abstractions;

namespace MoveBangladesh.NotificationService.BackgroundJob;

public abstract class BaseEventConsumer(
	IEventBus messageBus,
	INotifier notifier)
	: BackgroundService
{
	public override void Dispose()
	{
		base.Dispose();
		messageBus.Dispose();
	}
}
