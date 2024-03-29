using RideSharing.Domain.Entities;
using RideSharing.Domain.Enums;

namespace RideSharing.Domain.Factories
{
	public class PaymentFactory
	{
		public static Payment Create(Guid Id, Guid TripId, PaymentMethod Method, PaymentStatus Status, long Amount)
		{
			Payment payment = new()
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
