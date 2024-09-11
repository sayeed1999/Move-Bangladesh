using RideSharing.Domain.Common;

namespace RideSharing.Domain.Entities;

public abstract class BaseEntity : AuditableEntity
{
	public long Id { get; set; }

	public void ResetPrimaryKey()
	{
		// Note: EF Core auto generates Primary Key fields when Id is zero!
		Id = 0;
	}
}
