using RideSharing.Domain.Entities;

namespace RideSharing.Application.Abstractions
{
	public interface ITripRepository : IBaseRepository<TripEntity>
	{
		public Task<TripEntity> GetActiveTripForCustomer(Guid customerId);
		public Task<TripEntity> GetActiveTripForDriver(Guid driverId);
	}
}