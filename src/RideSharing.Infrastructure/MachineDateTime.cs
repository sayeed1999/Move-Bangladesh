using RideSharing.Common;

namespace RideSharing.Infrastructure;

public class MachineDateTime : IDateTime
{
	public DateTime Now => DateTime.UtcNow;

	public int CurrentYear => DateTime.UtcNow.Year;
}
