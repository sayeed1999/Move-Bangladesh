using RideSharing.ServiceBus.Abstractions;

namespace RideSharing.Common.MessageQueues.Messages
{
	public class TripRequestDto : Event
	{
		public TripRequestDto(
			Guid Id,
			Guid CustomerId,
			string Source,
			string Destination,
			string CabType,
			string PaymentMethod,
			string Status,
			Guid? DriverId = null)
		{
			this.Id = Id;
			this.CustomerId = CustomerId;
			this.Source = Source;
			this.Destination = Destination;
			this.CabType = CabType;
			this.PaymentMethod = PaymentMethod;
			this.Status = Status;
			this.DriverId = DriverId;
		}

		public Guid Id { get; }
		public Guid CustomerId { get; }
		public Guid? DriverId { get; }
		public string Source { get; }
		public string Destination { get; }
		public string CabType { get; }
		public string PaymentMethod { get; }
		public string Status { get; }
	}
}
