using CSharpFunctionalExtensions;
using FluentValidation;
using NetTopologySuite.Geometries;
using RideSharing.Domain.Enums;

namespace RideSharing.Domain.Entities;

public class Trip : BaseEntity
{
	public Guid TripRequestId { get; protected set; }
	public virtual TripRequest TripRequest { get; protected set; }
	public Guid CustomerId { get; protected set; }
	public virtual Customer Customer { get; protected set; }
	public Guid DriverId { get; protected set; }
	public virtual Driver Driver { get; protected set; }
	public PaymentMethod PaymentMethod { get; protected set; }
	public TripStatus TripStatus { get; protected set; }
	public Point Source { get; protected set; }
	public Point Destination { get; protected set; }
	public CabType CabType { get; protected set; }

	public static Result<Trip> Create(TripRequest tripRequest, Guid driverId)
	{
		return new Trip()
		{
			TripRequestId = tripRequest.Id,
			CustomerId = tripRequest.CustomerId,
			DriverId = driverId,
			PaymentMethod = tripRequest.PaymentMethod,
			TripStatus = TripStatus.DriverAccepted,
			Source = tripRequest.Source,
			Destination = tripRequest.Destination,
			CabType = tripRequest.CabType,
		};
	}

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

	private class TripValidator : AbstractValidator<Trip>
	{
		public TripValidator()
		{
			RuleFor(x => x.Source).NotEmpty();
			RuleFor(x => x.Destination).NotEmpty();
		}
	}
}
