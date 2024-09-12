using RideSharing.Domain.Entities;

namespace RideSharing.Application.Abstractions
{
	public interface ITripRepository : IBaseRepository<Trip>
	{
		Task<Trip> GetActiveTripForCustomer(string customerId);
		Task<Trip> GetActiveTripForDriver(string driverId);
		Task<Trip> HasOngoingTrip(string tripId, string driverId);
		Task<Trip> HasTripWaitingForPayment(string tripId, string customerId);
	}
}
