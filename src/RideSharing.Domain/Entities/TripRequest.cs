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


	public static Result<TripRequest> Cancel(TripRequest tripRequest)
	{
		tripRequest.Status = TripRequestStatus.CustomerCanceledBeforeDriverFound;
		tripRequest.UpdatedAt = DateTime.UtcNow;

		return Result.Success(tripRequest);
	}

	public static Result<TripRequest> DriverAccepted(TripRequest tripRequest)
	{
		tripRequest.Status = TripRequestStatus.DriverAccepted;
		tripRequest.UpdatedAt = DateTime.UtcNow;

		return Result.Success(tripRequest);
	}

	public static TripRequestDto GetTripRequestDto(TripRequest tripRequest)
	{
		var dto = new TripRequestDto(
			tripRequest.Id,
			tripRequest.CustomerId,
			tripRequest.Source.ToText(),
			tripRequest.Destination.ToText(),
			nameof(tripRequest.CabType),
			nameof(tripRequest.PaymentMethod),
			nameof(tripRequest.Status));

		return dto;
	}
}
