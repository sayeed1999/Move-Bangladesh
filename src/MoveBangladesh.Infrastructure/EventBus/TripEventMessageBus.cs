using Microsoft.Extensions.Logging;
using MoveBangladesh.Application.Abstractions;
using MoveBangladesh.ServiceBus.RabbitMQ;

namespace MoveBangladesh.Infrastructure.EventBus
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
