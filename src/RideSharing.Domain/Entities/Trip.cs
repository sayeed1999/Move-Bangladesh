using CSharpFunctionalExtensions;
using NetTopologySuite.Geometries;
using RideSharing.Common.MessageQueues.Messages;
using RideSharing.Domain.Enums;

namespace RideSharing.Domain.Entities;

public class Trip : BaseEntity
{
	public Guid TripRequestId { get; set; }
	public virtual TripRequest TripRequest { get; set; }
	public Guid CustomerId { get; set; }
	public virtual Customer Customer { get; set; }
	public Guid DriverId { get; set; }
	public virtual Driver Driver { get; set; }
	public PaymentMethod PaymentMethod { get; set; }
	public TripStatus TripStatus { get; set; }
	public Point Source { get; set; }
	public Point Destination { get; set; }
	public CabType CabType { get; set; }

	public Result Modify(TripStatus status)
	{
		TripStatus = status;

		return Result.Success();
	}

	public Result CancelByCustomer()
	{
		if (TripStatus >= TripStatus.TripStarted)
		{
			return Result.Failure<Trip>("Cannot cancel started trip. Contact customer care at +880***.");
		}

		TripStatus = TripStatus.CustomerCanceled;

		return Result.Success();
	}

	public Result CancelByDriver()
	{
		if (TripStatus >= TripStatus.TripStarted)
		{
			return Result.Failure<Trip>("Cannot cancel started trip. Contact customer care at +880***.");
		}

		TripStatus = TripStatus.DriverCanceled;

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
