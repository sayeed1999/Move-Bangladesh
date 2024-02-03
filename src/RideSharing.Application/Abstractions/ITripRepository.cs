using RideSharing.Domain.Entities;

namespace RideSharing.Application.Abstractions
{
	public interface ITripRepository : IBaseRepository<Trip>
	{
		public Task<Trip> GetActiveTrip(Guid customerId);
	}
}