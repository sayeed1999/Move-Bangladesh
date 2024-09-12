using RideSharing.Domain.Common;

namespace RideSharing.Domain.Entities;

public abstract class BaseEntity : AuditableEntity
{
	public string Id { get; set; } = String.Empty;

	public void GeneratePrimaryKey()
	{
		Id = Guid.NewGuid().ToString();
	}
}
