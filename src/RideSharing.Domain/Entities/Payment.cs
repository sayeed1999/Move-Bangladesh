using RideSharing.Domain.Enums;

namespace RideSharing.Domain.Entities
{
	public class Payment : BaseEntity
	{
		public Guid TripId { get; set; }
		public virtual Trip Trip { get; set; }
		public PaymentMethod PaymentMethod { get; set; }
		public PaymentStatus PaymentStatus { get; set; }
		public long Amount { get; set; }
	}
}