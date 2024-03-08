using RideSharing.ServiceBus.Abstractions;

namespace RideSharing.Common.MessageQueues.Messages
{
	public class TripDto(
		Guid Id,
		Guid TripRequestId,
		Guid CustomerId,
		Guid DriverId,
		string PaymentMethod,
		string TripStatus,
		string Source,
		string Destination,
		string CabType) : Event
	{
	}
}
