using RideSharing.Domain.Entities;
using RideSharing.Domain.Enums;

namespace RideSharing.Domain.Factories
{
	public class CabFactory
	{
		public static Cab Create(string regNo, Guid driverId, CabType type)
		{
			Cab cab = new Cab
			{
				RegNo = regNo,
				DriverId = driverId,
				CabType = type,
			};

			return cab;
		}
	}
}
