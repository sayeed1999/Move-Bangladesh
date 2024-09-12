using RideSharing.Domain.Entities;

namespace RideSharing.Application.Abstractions
{
	public interface ITripRequestRepository : IBaseRepository<TripRequest>
	{
		public Task<TripRequest> GetActiveTripRequestForCustomer(string customerId);
		public Task<TripRequest> GetActiveTripRequestForDriver(string driverId);
	}
}
