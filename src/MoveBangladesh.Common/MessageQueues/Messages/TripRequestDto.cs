using MoveBangladesh.ServiceBus.Abstractions;

namespace MoveBangladesh.Common.MessageQueues.Messages
{
	public class TripRequestDto : Event
	{
		public TripRequestDto(
			string Id,
			string CustomerId,
			string Source,
			string Destination,
			string CabType,
			string PaymentMethod,
			string Status,
			string? DriverId = null)
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

		public string Id { get; }
		public string CustomerId { get; }
		public string? DriverId { get; }
		public string Source { get; }
		public string Destination { get; }
		public string CabType { get; }
		public string PaymentMethod { get; }
		public string Status { get; }
	}
}
