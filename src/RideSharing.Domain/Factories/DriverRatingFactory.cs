using RideSharing.Domain.Entities;

namespace RideSharing.Domain.Factories
{
	public class DriverRatingFactory
	{
		public static DriverRatingEntity Create(Guid id, Guid customerId, Guid driverId, Guid tripId, short ratingValue, string feedback)
		{
			DriverRatingEntity driverRating = new DriverRatingEntity
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
