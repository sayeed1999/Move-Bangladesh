using RideSharing.Domain.Entities;

namespace RideSharing.Application.Abstractions
{
	public interface ITripRequestRepository : IBaseRepository<TripRequest>
	{
		public Task<TripRequest> GetActiveTripRequest(Guid customerId);
	}
}
