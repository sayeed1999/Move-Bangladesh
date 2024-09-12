using CSharpFunctionalExtensions;
using FluentValidation;
using RideSharing.Domain.ValueObjects;
using RideSharing.Domain.Entities;

namespace RideSharing.Domain.Factories
{
	public class CustomerFactory
	{
		public static Result<Customer> Create(string id, string userId, string name, Email email, string phoneNumber, string location)
		{
			Customer customer = new Customer()
			{
				Id = id,
				Name = name,
				Email = email.Value,
				PhoneNumber = phoneNumber,
				Location = location,
			};

			var validator = new CustomerValidator();
			var validationResult = validator.Validate(customer);

			if (validationResult.IsValid) return Result.Success(customer);
			return Result.Failure<Customer>("not valid");
		}

		private class CustomerValidator : AbstractValidator<Customer>
		{
			public CustomerValidator()
			{
				RuleFor(customer => customer.Email).EmailAddress();
			}
		}
	}
}
