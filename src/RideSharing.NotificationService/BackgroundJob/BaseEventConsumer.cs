using Microsoft.Extensions.Hosting;
using RideSharing.NotificationService.Notifier;
using RideSharing.ServiceBus.Abstractions;

namespace RideSharing.NotificationService.BackgroundJob;

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
