using CSharpFunctionalExtensions;
using FluentValidation;
using RideSharing.Common.ValueObjects;

namespace RideSharing.Domain.Entities;

public class Customer : Human
{
	public Customer() : base() { }

	public Customer(long id, long userId, string name, Email email, string phone, string location)
		: base(id, userId, name, email, phone, location)
	{

	}

	public virtual HashSet<DriverRating> DriverRatings { get; protected set; } = new();
	public virtual HashSet<Trip> Trips { get; protected set; } = new();

	public static Result<Customer> Create(long id, long userId, string name, Email email, string phone, string location)
	{
		Customer customer = new(id, userId, name, email, phone, location);

		var validator = new CustomerValidator();
		var validationResult = validator.Validate(customer);

		if (validationResult.IsValid) return Result.Success(customer);
		return Result.Failure<Customer>("not valid");
	}

	private class CustomerValidator : AbstractValidator<Customer>
	{
		public CustomerValidator()
		{
			RuleFor(customer => customer.Id).GreaterThanOrEqualTo(0);
			RuleFor(customer => customer.Email).EmailAddress();
		}
	}
}
