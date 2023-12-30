namespace RideSharing.Domain.Entities;

public class DriverRating : Rating
{
	public DriverRating() : base() { }

	public DriverRating(long id, long customerId, long driverId, long tripId, short ratingValue, string feedback)
		: base(id, customerId, driverId, tripId, ratingValue, feedback)
	{

	}

	public static DriverRating Create(long id, long customerId, long driverId, long tripId, short ratingValue, string feedback)
	{
		DriverRating driverRating = new(id, customerId, driverId, tripId, ratingValue, feedback);

		return driverRating;
	}
}