namespace RideSharing.Domain.Entities;

public abstract class Rating
{
	protected Rating() { }

	protected Rating(long id, long customerId, long driverId, long tripId, short ratingValue, string feedback)
	{
		Id = id;
		CustomerId = customerId;
		DriverId = driverId;
		TripId = tripId;
		RatingValue = ratingValue;
		Feedback = feedback;
	}

	public long Id { get; protected set; }
	public long CustomerId { get; protected set; }
	public virtual Customer Customer { get; protected set; }
	public long DriverId { get; protected set; }
	public virtual Driver Driver { get; protected set; }
	public long TripId { get; protected set; }
	public virtual Trip Trip { get; protected set; }
	public short RatingValue { get; protected set; }
	public string Feedback { get; protected set; }
}
