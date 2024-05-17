using RideSharing.Common.Entities;

namespace RideSharing.Domain.Entities;

public abstract class BaseEntity : AuditableEntity
{
	public long Id { get; set; }
}
