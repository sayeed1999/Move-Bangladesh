using CSharpFunctionalExtensions;
using FluentValidation;
using RideSharing.Domain.ValueObjects;
using RideSharing.Domain.Entities;

namespace RideSharing.Domain.Factories
{
	public class CustomerFactory
	{
		public static Result<CustomerEntity> Create(string id, string userId, string name, Email email, string phoneNumber, string location)
		{
			CustomerEntity customer = new CustomerEntity()
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
			return Result.Failure<CustomerEntity>("not valid");
		}

		private class CustomerValidator : AbstractValidator<CustomerEntity>
		{
			public CustomerValidator()
			{
				RuleFor(customer => customer.Email).EmailAddress();
			}
		}
	}
}
