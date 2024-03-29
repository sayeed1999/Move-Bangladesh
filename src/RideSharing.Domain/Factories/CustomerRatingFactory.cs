using RideSharing.Domain.Entities;

namespace RideSharing.Domain.Factories
{
	public class CustomerRatingFactory
	{
		public static CustomerRating Create(Guid id, Guid customerId, Guid driverId, Guid tripId, short ratingValue, string feedback)
		{
			CustomerRating customerRating = new CustomerRating
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
