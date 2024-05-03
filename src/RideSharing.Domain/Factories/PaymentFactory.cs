using RideSharing.Domain.Entities;

namespace RideSharing.Domain.Factories
{
	public class PaymentFactory
	{
		public static PaymentEntity Create(Guid Id, Guid TripId, PaymentMethod Method, PaymentStatus Status, long Amount)
		{
			PaymentEntity payment = new()
			{
				Id = Id,
				TripId = TripId,
				PaymentMethod = Method,
				PaymentStatus = Status,
				Amount = Amount,
			};

			return payment;
		}
	}
}
