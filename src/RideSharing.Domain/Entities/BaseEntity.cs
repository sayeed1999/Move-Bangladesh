using RideSharing.Domain.Common;

namespace RideSharing.Domain.Entities;

public abstract class BaseEntity : AuditableEntity
{
	public string Id { get; set; }

	public void GeneratePrimaryKey()
	{
		Id = Guid.NewGuid().ToString();
	}
}
