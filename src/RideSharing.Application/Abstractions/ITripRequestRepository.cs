using RideSharing.Domain.Entities;

namespace RideSharing.Application.Abstractions
{
	public interface ITripRequestRepository : IBaseRepository<TripRequestEntity>
	{
		public Task<TripRequestEntity> GetActiveTripRequestForCustomer(long customerId);
		public Task<TripRequestEntity> GetActiveTripRequestForDriver(long driverId);
	}
}
