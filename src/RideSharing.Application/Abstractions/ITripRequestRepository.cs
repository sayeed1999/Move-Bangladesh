using RideSharing.Domain.Entities;

namespace RideSharing.Application.Abstractions
{
	public interface ITripRequestRepository : IBaseRepository<TripRequestEntity>
	{
		public Task<TripRequestEntity> GetActiveTripRequestForCustomer(Guid customerId);
		public Task<TripRequestEntity> GetActiveTripRequestForDriver(Guid driverId);
	}
}
