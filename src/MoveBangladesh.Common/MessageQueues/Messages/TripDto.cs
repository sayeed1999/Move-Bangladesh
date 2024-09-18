using MoveBangladesh.ServiceBus.Abstractions;

namespace MoveBangladesh.Common.MessageQueues.Messages
{
	public class TripDto(
		string Id,
		string TripRequestId,
		string CustomerId,
		string DriverId,
		string PaymentMethod,
		string TripStatus,
		string Source,
		string Destination,
		string CabType) : Event
	{
	}
}
