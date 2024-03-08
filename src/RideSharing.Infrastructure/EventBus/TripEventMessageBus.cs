using RideSharing.Application.Abstractions;
using RideSharing.Domain.Entities;
using RideSharing.ServiceBus.RabbitMQ;

namespace RideSharing.Infrastructure.EventBus
{
	public class TripEventMessageBus : RabbitMQEventBus, ITripEventMessageBus
	{
		public override Task PublishAsync<T>(T integrationEvent, string queue = "", CancellationToken cancellationToken = default)
		{
			// overriding queue name
			return base.PublishAsync(integrationEvent, nameof(Trip), cancellationToken);
		}

		public override Task ConsumeAsync<T>(Func<T, Task> handleMessage, string queue = "", CancellationToken cancellationToken = default)
		{
			// overriding queue name
			return base.ConsumeAsync(handleMessage, nameof(Trip), cancellationToken);
		}
	}
}
