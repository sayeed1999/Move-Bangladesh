namespace RideSharing.Domain.Entities;

// This schema is the customer ratings submitted by drivers
public class CustomerRating : Rating
{
	public CustomerRating() : base() { }

	public CustomerRating(long id, long customerId, long driverId, long tripId, short ratingValue, string feedback)
		: base(id, customerId, driverId, tripId, ratingValue, feedback)
	{

	}

	public static CustomerRating Create(long id, long customerId, long driverId, long tripId, short ratingValue, string feedback)
	{
		CustomerRating customerRating = new(id, customerId, driverId, tripId, ratingValue, feedback);

		return customerRating;
	}
}