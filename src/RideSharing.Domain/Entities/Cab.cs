using RideSharing.Domain.Enums;

namespace RideSharing.Domain.Entities;

public class Cab : BaseEntity
{
	public string RegNo { get; protected set; }
	public Guid DriverId { get; protected set; }
	public virtual Driver Driver { get; protected set; }
	public CabType CabType { get; protected set; }

	public static Cab Create(string regNo, Guid driverId, CabType type)
	{
		Cab cab = new()
		{
			RegNo = regNo,
			DriverId = driverId,
			CabType = type,
		};

		return cab;
	}
}
