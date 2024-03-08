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
			string Status)
		{
			this.Id = Id;
			this.CustomerId = CustomerId;
			this.Source = Source;
			this.Destination = Destination;
			this.CabType = CabType;
			this.PaymentMethod = PaymentMethod;
			this.Status = Status;
		}

		public Guid Id { get; }
		public Guid CustomerId { get; }
		public string Source { get; }
		public string Destination { get; }
		public string CabType { get; }
		public string PaymentMethod { get; }
		public string Status { get; }
	}
}
