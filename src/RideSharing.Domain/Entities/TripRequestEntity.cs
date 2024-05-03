using CSharpFunctionalExtensions;
using NetTopologySuite.Geometries;
using RideSharing.Common.MessageQueues.Messages;

namespace RideSharing.Domain.Entities;

public class TripRequestEntity : BaseEntity
{
	public Guid CustomerId { get; set; }
	public virtual CustomerEntity Customer { get; set; }
	public Point Source { get; set; }
	public Point Destination { get; set; }
	public CabType CabType { get; set; }
	public PaymentMethod PaymentMethod { get; set; }
	public TripRequestStatus Status { get; set; }
	public virtual ICollection<TripRequestLogEntity> TripRequestLogs { get; set; }

	public Result Cancel()
	{
		Status = TripRequestStatus.CustomerCanceledBeforeDriverFound;
		UpdatedAt = DateTime.UtcNow;

		return Result.Success();
	}

	public Result DriverAccepted()
	{
		Status = TripRequestStatus.DriverAccepted;
		UpdatedAt = DateTime.UtcNow;

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
			nameof(Status));

		return dto;
	}
}

// Note: enum value a customer can only go to next stage, not before.
public enum TripRequestStatus
{
	NoDriverAccepted = 1, // finding driver
	CustomerCanceledBeforeDriverFound = 2, // lock a trip request once it reaches this stage
	DriverAccepted = 3, // driver may cancel, do not lock
	CustomerCanceledAfterDriverFound = 4, // lock a trip request once it reaches this stage
	DriverCanceled = 5, // should again find driver like NoDriverAccepted stage upto 3 times!
	TripStarted = 6, // lock a trip request once it reaches this stage
	TripRequestRejected = 7 // if no driver was found for this trip
}
