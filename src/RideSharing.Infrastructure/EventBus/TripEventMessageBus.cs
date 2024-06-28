using Microsoft.Extensions.Logging;
using RideSharing.Application.Abstractions;
using RideSharing.ServiceBus.RabbitMQ;

namespace RideSharing.Infrastructure.EventBus
{
	public class TripEventMessageBus : RabbitMQEventBus, ITripEventMessageBus
	{
		public TripEventMessageBus(ILogger<TripEventMessageBus> logger) : base(logger)
		{
		}

		public override Task PublishAsync<TripDto>(TripDto integrationEvent, string queue = "", CancellationToken cancellationToken = default)
		{
			// overriding queue name
			return base.PublishAsync(integrationEvent, nameof(TripDto), cancellationToken);
		}

		public override Task ConsumeAsync<TripDto>(Func<TripDto, Task> handleMessage, string queue = "", CancellationToken cancellationToken = default)
		{
			// overriding queue name
			return base.ConsumeAsync(handleMessage, nameof(TripDto), cancellationToken);
		}
	}
}
