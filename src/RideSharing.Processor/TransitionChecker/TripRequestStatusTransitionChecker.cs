using RideSharing.Domain.Entities;

namespace RideSharing.Processor.TransitionChecker;

public class TripRequestStatusTransitionChecker : ITransitionChecker<TripRequestStatus>
{
	private Dictionary<TripRequestStatus, List<TripRequestStatus>> _tripRequestMap;

	public TripRequestStatusTransitionChecker()
	{
		_tripRequestMap = new Dictionary<TripRequestStatus, List<TripRequestStatus>>
		{
			{
				TripRequestStatus.NoDriverAccepted,
				new List<TripRequestStatus>()
				{
					TripRequestStatus.CustomerCanceledBeforeDriverFound,
					TripRequestStatus.DriverAccepted,
				}
			},
			{
				TripRequestStatus.CustomerCanceledBeforeDriverFound,
				new List<TripRequestStatus>() // cannot move from this status!!
			},
			{
				TripRequestStatus.DriverAccepted,
				new List<TripRequestStatus>()
				{
					TripRequestStatus.CustomerCanceledAfterDriverFound,
					TripRequestStatus.DriverCanceled,
				}
			},
			{
				TripRequestStatus.CustomerCanceledAfterDriverFound,
				new List<TripRequestStatus>() // cannot move from this status!!
			},
			{
				TripRequestStatus.DriverCanceled,
				// Note: driver canceled should act same to same like no driver found
				new List<TripRequestStatus>()
				{
					TripRequestStatus.CustomerCanceledBeforeDriverFound,
					TripRequestStatus.DriverAccepted,
				}
			},
			{
				TripRequestStatus.TripStarted,
				new List<TripRequestStatus>() // cannot move from this status!!
			},
			{
				TripRequestStatus.TripRequestRejected,
				new List<TripRequestStatus>() // cannot move from this status!!
			}
		};
	}

	public bool IsTransitionValid(TripRequestStatus fromStatus, TripRequestStatus toStatus)
	{
		if (!_tripRequestMap.ContainsKey(fromStatus))
		{
			throw new NotImplementedException(
				$"Please report support team why transition of {nameof(TripRequestStatus)} from {Enum.GetName(fromStatus)} to {Enum.GetName(toStatus)} is not supported.");
		}

		var supportedStatuses = _tripRequestMap[fromStatus];

		var index = supportedStatuses.FindIndex(x => x == toStatus);

		return index >= 0;
	}
}
