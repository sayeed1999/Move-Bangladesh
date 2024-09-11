namespace RideSharing.Domain.Common;

public abstract class AuditableEntity
{
	public DateTime CreatedAt { get; private set; }
	public DateTime? LastModifiedAt { get; private set; }
	public DateTime? DeletedAt { get; private set; }

	public void Created()
	{
		CreatedAt = DateTime.UtcNow;
	}

	public void Modified()
	{
		LastModifiedAt = DateTime.UtcNow;
	}

	public void Deleted()
	{
		Modified();

		DeletedAt = LastModifiedAt;
	}
}
