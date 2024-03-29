using CSharpFunctionalExtensions;
using FluentValidation;
using RideSharing.Common.Enums;
using RideSharing.Common.ValueObjects;
using RideSharing.Domain.Entities;

namespace RideSharing.Domain.Factories
{
	public class DriverFactory
	{
		public static Result<Driver> Create(Guid id, Guid userId, string name, Gender gender, Email email, string phone, string location)
		{
			Driver driver = new Driver
			{
				Id = id,
				UserId = userId,
				Name = name,
				Email = email.Value,
				Phone = phone,
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
