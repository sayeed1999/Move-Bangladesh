using RideSharing.Domain.Entities;

namespace RideSharing.Application.Abstractions
{
	public interface ITripRequestRepository : IBaseRepository<TripRequestEntity>
	{
		public Task<TripRequestEntity> GetActiveTripRequestForCustomer(string customerId);
		public Task<TripRequestEntity> GetActiveTripRequestForDriver(string driverId);
	}
}
