using CSharpFunctionalExtensions;
using MoveBangladesh.Common.MessageQueues.Messages;
using System.Drawing;

namespace MoveBangladesh.Domain.Entities;

public class TripRequest : BaseEntity
{
	public required string CustomerId { get; set; }
	public virtual Customer? Customer { get; set; }
	public string? DriverId { get; set; }
	public virtual Driver? Driver { get; set; }
	public float SourceX { get; set; }
	public float SourceY { get; set; }
	public float DestinationX { get; set; }
	public float DestinationY { get; set; }
	public CabType CabType { get; set; }
	public PaymentMethod PaymentMethod { get; set; }
	public TripRequestStatus Status { get; set; }

	public TripRequestDto GetTripRequestDto()
	{
		var dto = new TripRequestDto(
			Id,
			CustomerId,
			"",
			"",
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
