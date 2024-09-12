using CSharpFunctionalExtensions;
using RideSharing.Common.MessageQueues.Messages;

namespace RideSharing.Domain.Entities;

public class Trip : BaseEntity
{
	public Trip()
	{
	}

	public Trip(TripRequest tripRequest, string driverId)
	{
		TripRequestId = tripRequest.Id;
		CustomerId = tripRequest.CustomerId;
		DriverId = driverId;
		PaymentMethod = tripRequest.PaymentMethod;
		TripStatus = TripStatus.ONGOING;
		SourceX = tripRequest.SourceX;
		SourceY = tripRequest.SourceY;
		DestinationX = tripRequest.DestinationX;
		DestinationY = tripRequest.DestinationY;
		CabType = tripRequest.CabType;
	}

	public string TripRequestId { get; set; } = string.Empty;
	public virtual TripRequest? TripRequest { get; set; }
	public string CustomerId { get; set; } = string.Empty;
	public virtual Customer? Customer { get; set; }
	public string DriverId { get; set; } = string.Empty;
	public virtual Driver? Driver { get; set; }
	public PaymentMethod PaymentMethod { get; set; }
	public TripStatus TripStatus { get; set; }
	public float SourceX { get; set; }
	public float SourceY { get; set; }
	public float DestinationX { get; set; }
	public float DestinationY { get; set; }
	public CabType CabType { get; set; }
	public virtual ICollection<Payment>? Payments { get; private set; }
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
			"",
			"",
			nameof(CabType));
	}
}

public enum TripStatus
{
	ONGOING = 1,
	WAITING_FOR_PAYMENT = 2, // this is when driver reached destination
	PAYMENT_COMPLETED = 3,
}
