using MoveBangladesh.Domain.Entities;

namespace MoveBangladesh.Application.Abstractions
{
	public interface IRideProcessingService
	{
		Task<bool> IsTripRequestTransitionValid(TripRequestStatus fromStatus, TripRequestStatus toStatus);
		Task<bool> IsTripTransitionValid(TripStatus fromStatus, TripStatus toStatus);
	}
}
