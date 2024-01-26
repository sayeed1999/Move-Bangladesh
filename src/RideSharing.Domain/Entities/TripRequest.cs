using CSharpFunctionalExtensions;
using FluentValidation;
using NetTopologySuite.Geometries;
using RideSharing.Domain.Enums;

namespace RideSharing.Domain.Entities;

public class TripRequest : BaseEntity
{
	public Guid CustomerId { get; protected set; }
	public virtual Customer Customer { get; protected set; }
	public Point Source { get; protected set; }
	public Point Destination { get; protected set; }
	public CabType CabType { get; protected set; }
	public PaymentMethod PaymentMethod { get; protected set; }
	public TripRequestStatus Status { get; protected set; }

	/// <summary>
	/// Use this method when a customer wants to request for a trip.
	/// </summary>
	/// <param name="customerId"></param>
	/// <param name="source"></param>
	/// <param name="destination"></param>
	/// <param name="cabType"></param>
	/// <param name="paymentMethod"></param>
	/// <returns></returns>
	public static Result<TripRequest> Create(
	Guid customerId,
	Tuple<double,
	double> source,
	Tuple<double, double> destination,
	CabType cabType,
	PaymentMethod paymentMethod)
	{
		var x = new TripRequest()
		{
			CustomerId = customerId,
			Source = new Point(source.Item1, source.Item2),
			Destination = new Point(destination.Item1, destination.Item2),
			CabType = cabType,
			PaymentMethod = paymentMethod,
			Status = TripRequestStatus.NoDriverAccepted, // This should be the default type if no driver accepts the ride.
			CreatedAt = DateTime.UtcNow,
			UpdatedAt = DateTime.UtcNow, // considering CreatedAt == UpdatedAt when trip is requested
		};

		var validator = new TripRequestValidator();
		var result = validator.Validate(x);

		if (result.IsValid) return Result.Success(x);
		return Result.Failure<TripRequest>("Model is invalid");
	}

	public static Result<TripRequest> Cancel(TripRequest tripRequest)
	{
		tripRequest.Status = TripRequestStatus.CustomerCanceledBeforeDriverFound;
		tripRequest.UpdatedAt = DateTime.UtcNow;

		return Result.Success(tripRequest);
	}

	private class TripRequestValidator : AbstractValidator<TripRequest>
	{
		public TripRequestValidator()
		{
			RuleFor(x => x.Source).NotEmpty();
			RuleFor(x => x.Destination).NotEmpty();
		}
	}
}
