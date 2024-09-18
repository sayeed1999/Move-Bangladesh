using MoveBangladesh.Domain.Common;

namespace MoveBangladesh.Domain.Entities;

public abstract class BaseEntity : AuditableEntity
{
	public string Id { get; set; } = string.Empty;

	public void GeneratePrimaryKey()
	{
		Id = Guid.NewGuid().ToString();
	}
}
