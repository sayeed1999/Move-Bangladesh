using RideSharing.Domain.Entities;

namespace RideSharing.Domain.Factories
{
	public class PaymentFactory
	{
		public static PaymentEntity Create(string Id, string TripId, PaymentMethod Method, PaymentStatus Status, int Amount)
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
