using CSharpFunctionalExtensions;
using FluentValidation;
using RideSharing.Common.ValueObjects;
using RideSharing.Domain.Entities;

namespace RideSharing.Domain.Factories
{
	public class CustomerFactory
	{
		public static Result<CustomerEntity> Create(long id, long userId, string name, Email email, string phone, string location)
		{
			CustomerEntity customer = new CustomerEntity()
			{
				Id = id,
				Name = name,
				Email = email.Value,
				Phone = phone,
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
