using RideSharing.ServiceBus.Abstractions;

namespace RideSharing.Common.MessageQueues.Messages
{
	public class TripDto(
		long Id,
		long TripRequestId,
		long CustomerId,
		long DriverId,
		string PaymentMethod,
		string TripStatus,
		string Source,
		string Destination,
		string CabType) : Event
	{
	}
}
