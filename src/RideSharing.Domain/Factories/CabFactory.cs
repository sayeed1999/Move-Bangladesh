using RideSharing.Domain.Entities;

namespace RideSharing.Domain.Factories
{
	public class CabFactory
	{
		public static CabEntity Create(string regNo, long driverId, CabType type)
		{
			CabEntity cab = new CabEntity
			{
				RegNo = regNo,
				DriverId = driverId,
				CabType = type,
			};

			return cab;
		}
	}
}
