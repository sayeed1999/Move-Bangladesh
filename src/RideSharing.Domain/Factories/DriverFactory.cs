using CSharpFunctionalExtensions;
using FluentValidation;
using RideSharing.Common.Enums;
using RideSharing.Domain.ValueObjects;
using RideSharing.Domain.Entities;

namespace RideSharing.Domain.Factories
{
	public class DriverFactory
	{
		public static Result<Driver> Create(string id, string userId, string name, Gender gender, Email email, string phoneNumber, string location)
		{
			Driver driver = new Driver
			{
				Id = id,
				Name = name,
				Email = email.Value,
				PhoneNumber = phoneNumber,
				Location = location,
			};

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
}
