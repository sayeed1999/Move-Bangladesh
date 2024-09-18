using MoveBangladesh.Common;

namespace MoveBangladesh.Infrastructure;

public class MachineDateTime : IDateTime
{
	public DateTime Now => DateTime.UtcNow;

	public int CurrentYear => DateTime.UtcNow.Year;
}
