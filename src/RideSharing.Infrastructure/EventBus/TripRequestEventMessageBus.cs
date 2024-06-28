using Microsoft.Extensions.Logging;
using RideSharing.Common.MessageQueues.Abstractions;
using RideSharing.ServiceBus.RabbitMQ;

namespace RideSharing.Infrastructure.EventBus
{
	public class TripRequestEventMessageBus : RabbitMQEventBus, ITripRequestEventMessageBus
	{
		public TripRequestEventMessageBus(ILogger<TripRequestEventMessageBus> logger) : base(logger)
		{
		}

		public override Task PublishAsync<TripRequestDto>(TripRequestDto integrationEvent, string queue = "", CancellationToken cancellationToken = default)
		{
			// overriding queue name
			return base.PublishAsync(integrationEvent, nameof(TripRequestDto), cancellationToken);
		}

		public override Task ConsumeAsync<TripRequestDto>(Func<TripRequestDto, Task> handleMessage, string queue = "", CancellationToken cancellationToken = default)
		{
			// overriding queue name
			return base.ConsumeAsync(handleMessage, nameof(TripRequestDto), cancellationToken);
		}
	}
}
