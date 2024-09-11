using CSharpFunctionalExtensions;
using NetTopologySuite.Geometries;
using RideSharing.Common.MessageQueues.Messages;

namespace RideSharing.Domain.Entities;

public class TripRequestEntity : BaseEntity
{
	public required string CustomerId { get; set; }
	public virtual CustomerEntity? Customer { get; set; }
	public string? DriverId { get; set; }
	public virtual DriverEntity? Driver { get; set; }
	public required Point Source { get; set; }
	public required Point Destination { get; set; }
	public CabType CabType { get; set; }
	public PaymentMethod PaymentMethod { get; set; }
	public TripRequestStatus Status { get; set; }
	public virtual ICollection<TripRequestLogEntity>? TripRequestLogs { get; private set; }

	public Result Modify(TripRequestStatus status, string? driverId = null)
	{
		if (driverId is not null)
		{
			DriverId = driverId;
		}

		Status = status;

		return Result.Success();
	}

	public TripRequestDto GetTripRequestDto()
	{
		var dto = new TripRequestDto(
			Id,
			CustomerId,
			Source.ToText(),
			Destination.ToText(),
			nameof(CabType),
			nameof(PaymentMethod),
			nameof(Status),
			DriverId);

		return dto;
	}
}

public enum TripRequestStatus
{
	NO_DRIVER_FOUND = 1,
	CUSTOMER_CANCELED = 2,
	DRIVER_ACCEPTED = 3,
	CUSTOMER_REJECTED_DRIVER = 4,
	DRIVER_REJECTED_CUSTOMER = 5, // TODO: upon 3 driver rejections, take the trip request to trip_request_rejected
	TRIP_STARTED = 6, // create a trip entity where status reaches this stage
	TRIP_REQUEST_REJECTED = 7 // TODO: run a scheduler and send all unaccepted trips to this status
}
