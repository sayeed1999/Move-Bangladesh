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
				TripRequestStatus.NO_DRIVER_FOUND,
				new List<TripRequestStatus>()
				{
					TripRequestStatus.CUSTOMER_CANCELED,
					TripRequestStatus.DRIVER_ACCEPTED,
				}
			},
			{
				TripRequestStatus.CUSTOMER_CANCELED,
				new List<TripRequestStatus>() // cannot move from this status!!
			},
			{
				TripRequestStatus.DRIVER_ACCEPTED,
				new List<TripRequestStatus>()
				{
					TripRequestStatus.CUSTOMER_REJECTED_DRIVER,
					TripRequestStatus.DRIVER_REJECTED_CUSTOMER,
				}
			},
			{
				TripRequestStatus.CUSTOMER_REJECTED_DRIVER,
				new List<TripRequestStatus>() // cannot move from this status!!
			},
			{
				// TODO: on first two driver cancel, it should reset to NoDriverFound, on 3rd cancel it should become locked!
				TripRequestStatus.DRIVER_REJECTED_CUSTOMER,
				new List<TripRequestStatus>()
				{
					TripRequestStatus.NO_DRIVER_FOUND,
				}
			},
			{
				TripRequestStatus.TRIP_STARTED,
				new List<TripRequestStatus>() // cannot move from this status!!
			},
			{
				TripRequestStatus.TRIP_REQUEST_REJECTED,
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
