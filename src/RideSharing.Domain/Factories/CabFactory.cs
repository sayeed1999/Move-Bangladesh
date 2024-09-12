using RideSharing.Domain.Entities;

namespace RideSharing.Domain.Factories
{
	public class CabFactory
	{
		public static Cab Create(string regNo, string driverId, CabType type)
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
