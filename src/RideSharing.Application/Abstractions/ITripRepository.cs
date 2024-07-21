using RideSharing.Domain.Entities;

namespace RideSharing.Application.Abstractions
{
	public interface ITripRepository : IBaseRepository<TripEntity>
	{
		Task<TripEntity> GetActiveTripForCustomer(long customerId);
		Task<TripEntity> GetActiveTripForDriver(long driverId);
		Task<TripEntity> HasOngoingTrip(long tripId, long driverId);
	}
}
