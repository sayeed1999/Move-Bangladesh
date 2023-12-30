namespace RideSharing.Domain.Entities;

public abstract class Rating
{
	protected Rating() { }

	protected Rating(Guid id, Guid customerId, Guid driverId, Guid tripId, short ratingValue, string feedback)
	{
		Id = id;
		CustomerId = customerId;
		DriverId = driverId;
		TripId = tripId;
		RatingValue = ratingValue;
		Feedback = feedback;
	}

	public Guid Id { get; protected set; }
	public Guid CustomerId { get; protected set; }
	public virtual Customer Customer { get; protected set; }
	public Guid DriverId { get; protected set; }
	public virtual Driver Driver { get; protected set; }
	public Guid TripId { get; protected set; }
	public virtual Trip Trip { get; protected set; }
	public short RatingValue { get; protected set; }
	public string Feedback { get; protected set; }
}
