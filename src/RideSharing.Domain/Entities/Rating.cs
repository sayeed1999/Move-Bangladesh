namespace RideSharing.Domain.Entities;

public abstract class Rating
{
	public Guid Id { get; set; }
	public Guid CustomerId { get; set; }
	public virtual CustomerEntity Customer { get; set; }
	public Guid DriverId { get; set; }
	public virtual DriverEntity Driver { get; set; }
	public Guid TripId { get; set; }
	public virtual TripEntity Trip { get; set; }
	public short RatingValue { get; set; }
	public string Feedback { get; set; }
}
