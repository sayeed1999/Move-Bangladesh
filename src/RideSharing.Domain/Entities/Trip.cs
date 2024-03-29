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

	public static Result<Trip> Modify(Guid id, TripStatus status)
	{
		if (status == null) return Result.Failure<Trip>("Model is invalid");

		var x = new Trip()
		{
			Id = id,
			TripStatus = status,
		};

		return Result.Success(x);
	}

	public static Result<Trip> CancelByCustomer(Trip trip)
	{
		if (trip.TripStatus >= TripStatus.TripStarted)
		{
			return Result.Failure<Trip>("Cannot cancel started trip. Contact customer care at +880***.");
		}

		// TODO: dont modify arguments, bad practice!
		var modifiedTrip = trip;
		modifiedTrip.TripStatus = TripStatus.CustomerCanceled;

		return modifiedTrip;
	}

	public static Result<Trip> CancelByDriver(Trip trip)
	{
		if (trip.TripStatus >= TripStatus.TripStarted)
		{
			return Result.Failure<Trip>("Cannot cancel started trip. Contact customer care at +880***.");
		}

		// TODO: dont modify arguments, bad practice!
		var modifiedTrip = trip;
		modifiedTrip.TripStatus = TripStatus.DriverCanceled;

		return modifiedTrip;
	}

	public static TripDto GetTripDto(Trip trip)
	{
		return new TripDto(
			trip.Id,
			trip.TripRequestId,
			trip.CustomerId,
			trip.DriverId,
			nameof(trip.PaymentMethod),
			nameof(trip.TripStatus),
			trip.Source.ToText(),
			trip.Destination.ToText(),
			nameof(trip.CabType));
	}
}
