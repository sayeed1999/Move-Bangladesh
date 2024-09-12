using RideSharing.Domain.Entities;

namespace RideSharing.Domain.Factories
{
	public class CustomerRatingFactory
	{
		public static CustomerRating Create(string id, string customerId, string driverId, string tripId, short ratingValue, string feedback)
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
