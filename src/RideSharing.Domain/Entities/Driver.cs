using CSharpFunctionalExtensions;
using FluentValidation;
using RideSharing.Common.Enums;
using RideSharing.Common.ValueObjects;

namespace RideSharing.Domain.Entities;

public class Driver : Human
{
	public Driver() : base() { }

	public Driver(Guid id, Guid userId, string name, Email email, string phone, string location)
		: base(id, userId, name, email, phone, location)
	{

	}

	public virtual HashSet<CustomerRating> CustomerRatings { get; protected set; } = new();
	public virtual HashSet<Trip> Trips { get; protected set; } = new();

	public static Result<Driver> Create(Guid id, Guid userId, string name, Gender gender, Email email, string phone, string location)
	{
		Driver driver = new(id, userId, name, email, phone, location);

		var validator = new DriverValidator();
		var validationResult = validator.Validate(driver);

		if (validationResult.IsValid) return Result.Success(driver);
		return Result.Failure<Driver>("not valid");
	}

	private class DriverValidator : AbstractValidator<Driver>
	{
		public DriverValidator()
		{
			RuleFor(c => c.Email).EmailAddress();
		}
	}
}
