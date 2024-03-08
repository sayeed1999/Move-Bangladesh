using RideSharing.Application.Abstractions;
using RideSharing.ServiceBus.RabbitMQ;

namespace RideSharing.Infrastructure.EventBus
{
	public class TripEventPublisher : RabbitMQEventBus, ITripEventPublisher
	{

	}
}
