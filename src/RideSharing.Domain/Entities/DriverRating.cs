namespace RideSharing.Domain.Entities;

public class DriverRating : Rating
{
	public DriverRating() : base() { }

	public DriverRating(Guid id, Guid customerId, Guid driverId, Guid tripId, short ratingValue, string feedback)
		: base(id, customerId, driverId, tripId, ratingValue, feedback)
	{

	}

	public static DriverRating Create(Guid id, Guid customerId, Guid driverId, Guid tripId, short ratingValue, string feedback)
	{
		DriverRating driverRating = new(id, customerId, driverId, tripId, ratingValue, feedback);

		return driverRating;
	}
}