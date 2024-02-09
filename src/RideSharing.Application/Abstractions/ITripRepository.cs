using RideSharing.Domain.Entities;

namespace RideSharing.Application.Abstractions
{
	public interface ITripRepository : IBaseRepository<Trip>
	{
		public Task<Trip> GetActiveTripForCustomer(Guid customerId);
		public Task<Trip> GetActiveTripForDriver(Guid driverId);
	}
}