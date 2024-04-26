using RideSharing.Domain.Entities;

namespace RideSharing.Domain.Factories
{
	public class CustomerRatingFactory
	{
		public static CustomerRatingEntity Create(Guid id, Guid customerId, Guid driverId, Guid tripId, short ratingValue, string feedback)
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
