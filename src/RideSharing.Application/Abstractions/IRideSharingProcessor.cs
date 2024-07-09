using RideSharing.Domain.Entities;

namespace RideSharing.Application.Abstractions
{
	public interface IRideSharingProcessor
	{
        Task<bool> CheckTripRequestTransition(TripRequestStatus fromStatus, TripRequestStatus toStatus);
        Task<bool> CheckTripTransition(TripStatus fromStatus, TripStatus toStatus);        
	}
}
