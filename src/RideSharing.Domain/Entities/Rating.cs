namespace RideSharing.Domain.Entities;

public abstract class Rating
{
	public long Id { get; set; }
	public long CustomerId { get; set; }
	public virtual CustomerEntity Customer { get; set; }
	public long DriverId { get; set; }
	public virtual DriverEntity Driver { get; set; }
	public long TripId { get; set; }
	public virtual TripEntity Trip { get; set; }
	public short RatingValue { get; set; }
	public string? Feedback { get; set; }
}
