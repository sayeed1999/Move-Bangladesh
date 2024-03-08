namespace RideSharing.Domain.Entities;

public abstract class BaseEntity
{
	public BaseEntity()
	{
		CreatedAt = DateTime.UtcNow;
		UpdatedAt = DateTime.UtcNow;
	}

	public Guid Id { get; set; }
	public DateTime CreatedAt { get; set; }
	public Guid CreatedBy { get; set; }
	public DateTime? UpdatedAt { get; set; }
	public Guid? UpdatedBy { get; set; }
	public DateTime? DeletedAt { get; set; }
	public Guid? DeletedBy { get; set; }
}
