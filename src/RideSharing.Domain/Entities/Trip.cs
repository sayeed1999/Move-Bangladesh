using CSharpFunctionalExtensions;
using FluentValidation;
using RideSharing.Domain.Enums;

namespace RideSharing.Domain.Entities;

public class Trip : BaseEntity
{
	public long CustomerId { get; protected set; }
	public virtual Customer Customer { get; protected set; }
	public long DriverId { get; protected set; }
	public virtual Driver Driver { get; protected set; }
	public virtual Payment Payment { get; protected set; }
	public TripStatus TripStatus { get; protected set; }
	public String Source { get; protected set; }
	public String Destination { get; protected set; }

	public static Result<Trip> CreateNewTrip(long customerId, long driverId, string source, string destination)
	{
		var x = new Trip()
		{
			Id = 0,
			CustomerId = customerId,
			DriverId = driverId,
			Source = source,
			Destination = destination,
			TripStatus = TripStatus.TripRequested,
		};

		var validator = new TripValidator();
		var result = validator.Validate(x);

		if (result.IsValid) return Result.Success(x);
		return Result.Failure<Trip>("Model is invalid");
	}

	public static Result<Trip> Modify(long id, TripStatus status)
	{
		if (status == null || id <= 0) return Result.Failure<Trip>("Model is invalid");

		var x = new Trip()
		{
			Id = id,
			TripStatus = status,
		};

		return Result.Success(x);
	}

}

public class TripValidator : AbstractValidator<Trip>
{
	public TripValidator()
	{
		RuleFor(x => x.Id).GreaterThanOrEqualTo(0);
		RuleFor(x => x.CustomerId).GreaterThan(0);
		RuleFor(x => x.DriverId).GreaterThan(0);
		RuleFor(x => x.Source).NotEmpty();
		RuleFor(x => x.Destination).NotEmpty();
	}
}
