using RideSharing.Domain.Entities;

namespace RideSharing.Application.Abstractions
{
	public interface IRideProcessingService
	{
        Task<bool> IsTripRequestTransitionValid(TripRequestStatus fromStatus, TripRequestStatus toStatus);
        Task<bool> IsTripTransitionValid(TripStatus fromStatus, TripStatus toStatus);        
	}
}
