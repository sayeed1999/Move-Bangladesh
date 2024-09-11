using RideSharing.Domain.Entities;

namespace RideSharing.Application.Abstractions
{
	public interface ITripRepository : IBaseRepository<TripEntity>
	{
		Task<TripEntity> GetActiveTripForCustomer(string customerId);
		Task<TripEntity> GetActiveTripForDriver(string driverId);
		Task<TripEntity> HasOngoingTrip(string tripId, string driverId);
		Task<TripEntity> HasTripWaitingForPayment(string tripId, string customerId);
	}
}
