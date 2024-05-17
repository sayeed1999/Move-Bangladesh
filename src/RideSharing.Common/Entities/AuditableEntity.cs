namespace RideSharing.Common.Entities;

public abstract class AuditableEntity
{
	public DateTime CreatedAt { get; private set; }
	public DateTime? LastModifiedAt { get; private set; }
	public DateTime? DeletedAt { get; private set; }

	public void SetCreatedAt()
	{
		CreatedAt = DateTime.UtcNow;
	}

	public void UpdateLastModifiedAt()
	{
		LastModifiedAt = DateTime.UtcNow;
	}

	public void Delete()
	{
		UpdateLastModifiedAt();
		DeletedAt = DateTime.UtcNow;
	}
}
