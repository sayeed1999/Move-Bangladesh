using RideSharing.Domain.Entities;

namespace RideSharing.Domain.Factories
{
	public class DriverRatingFactory
	{
		public static DriverRating Create(string id, string customerId, string driverId, string tripId, short ratingValue, string feedback)
		{
			DriverRating driverRating = new DriverRating
			{
				Id = id,
				CustomerId = customerId,
				DriverId = driverId,
				TripId = tripId,
				RatingValue = ratingValue,
				Feedback = feedback,
			};

			return driverRating;
		}
	}
}
