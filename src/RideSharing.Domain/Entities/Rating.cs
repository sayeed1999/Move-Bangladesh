namespace RideSharing.Domain.Entities;

public abstract class Rating : BaseEntity
{
	public string CustomerId { get; set; } = string.Empty;
	public virtual Customer? Customer { get; set; }
	public string DriverId { get; set; } = string.Empty;
	public virtual Driver? Driver { get; set; }
	public string TripId { get; set; } = string.Empty;
	public virtual Trip? Trip { get; set; }
	public short RatingValue { get; set; }
	public string? Feedback { get; set; }
}
