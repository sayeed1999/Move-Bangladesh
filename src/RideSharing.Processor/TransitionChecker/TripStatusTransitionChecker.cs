using RideSharing.Domain.Entities;

namespace RideSharing.Processor.TransitionChecker;

public class TripStatusTransitionChecker : ITransitionChecker<TripStatus>
{
	private Dictionary<TripStatus, List<TripStatus>> _tripMap;

	public TripStatusTransitionChecker()
	{
		_tripMap = new Dictionary<TripStatus, List<TripStatus>>
		{
			{
				TripStatus.DriverAccepted,
				new List<TripStatus>()
				{
					TripStatus.CustomerCanceled,
					TripStatus.DriverCanceled,
					TripStatus.TripStarted,
				}
			},
			{
				TripStatus.CustomerCanceled,
				new List<TripStatus>() // cannot move to any status from here!!
			},
			{
				TripStatus.DriverCanceled,
				new List<TripStatus>() // cannot move to any status from here!!
			},
			{
				TripStatus.TripStarted,
				new List<TripStatus>()
				{
					TripStatus.TripCompleted,
				}
			},
			{
				TripStatus.TripCompleted,
				new List<TripStatus>() // cannot move to any status from here!!
			}
		};
	}

	public bool IsTransitionValid(TripStatus fromStatus, TripStatus toStatus)
	{
		if (!_tripMap.ContainsKey(fromStatus))
		{
			throw new NotImplementedException(
				$"Please report support team why transition of {nameof(TripStatus)} from {Enum.GetName(fromStatus)} to {Enum.GetName(toStatus)} is not supported.");
		}

		var supportedStatuses = _tripMap[fromStatus];

		var index = supportedStatuses.FindIndex(x => x == toStatus);

		return index >= 0;
	}
}
