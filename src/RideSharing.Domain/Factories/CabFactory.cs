using RideSharing.Domain.Entities;
using RideSharing.Domain.Enums;

namespace RideSharing.Domain.Factories
{
	public class CabFactory
	{
		public static CabEntity Create(string regNo, Guid driverId, CabType type)
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
