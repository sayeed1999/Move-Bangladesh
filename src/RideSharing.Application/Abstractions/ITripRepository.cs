using RideSharing.Domain.Entities;

namespace RideSharing.Application.Abstractions
{
	public interface ITripRepository : IBaseRepository<TripEntity>
	{
		public Task<TripEntity> GetActiveTripForCustomer(long customerId);
		public Task<TripEntity> GetActiveTripForDriver(long driverId);

	}
}
