using MoveBangladesh.Domain.Entities;

namespace MoveBangladesh.Processor.TransitionChecker;

public class TripStatusTransitionChecker : ITransitionChecker<TripStatus>
{
	private Dictionary<TripStatus, List<TripStatus>> _tripMap;

	public TripStatusTransitionChecker()
	{
		_tripMap = new Dictionary<TripStatus, List<TripStatus>>
		{
			{
				TripStatus.ONGOING,
				new List<TripStatus>()
				{
					TripStatus.WAITING_FOR_PAYMENT,
				}
			},
			{
				TripStatus.WAITING_FOR_PAYMENT,
				new List<TripStatus>()
				{
					TripStatus.PAYMENT_COMPLETED,
				}
			},
			{
				TripStatus.PAYMENT_COMPLETED,
				new List<TripStatus>()
			},
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
