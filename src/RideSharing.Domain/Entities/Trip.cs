using CSharpFunctionalExtensions;
using NetTopologySuite.Geometries;
using RideSharing.Common.MessageQueues.Messages;

namespace RideSharing.Domain.Entities;

public class Trip : BaseEntity
{
	public required string TripRequestId { get; set; }
	public virtual TripRequest? TripRequest { get; set; }
	public required string CustomerId { get; set; }
	public virtual Customer? Customer { get; set; }
	public required string DriverId { get; set; }
	public virtual Driver? Driver { get; set; }
	public PaymentMethod PaymentMethod { get; set; }
	public TripStatus TripStatus { get; set; }
	public required Point Source { get; set; }
	public required Point Destination { get; set; }
	public CabType CabType { get; set; }
	public virtual ICollection<Payment>? Payments { get; private set; }
	public virtual ICollection<TripLog>? TripLogs { get; private set; }

	public Result Modify(TripStatus status)
	{
		TripStatus = status;

		return Result.Success();
	}


	public TripDto GetTripDto()
	{
		return new TripDto(
			Id,
			TripRequestId,
			CustomerId,
			DriverId,
			nameof(PaymentMethod),
			nameof(TripStatus),
			Source.ToText(),
			Destination.ToText(),
			nameof(CabType));
	}
}

public enum TripStatus
{
	ONGOING = 1,
	WAITING_FOR_PAYMENT = 2, // this is when driver reached destination
	PAYMENT_COMPLETED = 3,
}
