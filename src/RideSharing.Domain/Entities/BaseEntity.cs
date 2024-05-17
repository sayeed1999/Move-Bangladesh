namespace RideSharing.Domain.Entities;

public abstract class BaseEntity
{
	public BaseEntity()
	{

	}

	public long Id { get; set; }
	public DateTime CreatedAt { get; set; }
	public DateTime? LastUpdatedAt { get; set; }
	public DateTime? DeletedAt { get; set; }
}
