namespace RideSharing.Domain.Entities;

public abstract class Rating : BaseEntity
{
	public required string CustomerId { get; set; }
	public virtual Customer? Customer { get; set; }
	public required string DriverId { get; set; }
	public virtual Driver? Driver { get; set; }
	public required string TripId { get; set; }
	public virtual Trip? Trip { get; set; }
	public short RatingValue { get; set; }
	public string? Feedback { get; set; }
}
