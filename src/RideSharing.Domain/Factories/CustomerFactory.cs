using CSharpFunctionalExtensions;
using FluentValidation;
using RideSharing.Common.ValueObjects;
using RideSharing.Domain.Entities;

namespace RideSharing.Domain.Factories
{
	public class CustomerFactory
	{
		public static Result<Customer> Create(Guid id, Guid userId, string name, Email email, string phone, string location)
		{
			Customer customer = new Customer()
			{
				Id = id,
				UserId = userId,
				Name = name,
				Email = email.Value,
				Phone = phone,
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
