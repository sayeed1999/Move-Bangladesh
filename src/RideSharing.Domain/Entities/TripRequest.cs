using CSharpFunctionalExtensions;
using NetTopologySuite.Geometries;
using RideSharing.Common.MessageQueues.Messages;
using RideSharing.Domain.Enums;

namespace RideSharing.Domain.Entities;

public class TripRequest : BaseEntity
{
	public Guid CustomerId { get; set; }
	public virtual Customer Customer { get; set; }
	public Point Source { get; set; }
	public Point Destination { get; set; }
	public CabType CabType { get; set; }
	public PaymentMethod PaymentMethod { get; set; }
	public TripRequestStatus Status { get; set; }


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
