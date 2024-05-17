using RideSharing.Domain.Entities;

namespace RideSharing.Domain.Factories
{
	public class CustomerRatingFactory
	{
		public static CustomerRatingEntity Create(long id, long customerId, long driverId, long tripId, short ratingValue, string feedback)
		{
			CustomerRatingEntity customerRating = new CustomerRatingEntity
			{
				Id = id,
				CustomerId = customerId,
				DriverId = driverId,
				TripId = tripId,
				RatingValue = ratingValue,
				Feedback = feedback,
			};

			return customerRating;
		}
	}
}
