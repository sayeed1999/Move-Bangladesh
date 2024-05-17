using CSharpFunctionalExtensions;
using FluentValidation;
using RideSharing.Common.Enums;
using RideSharing.Common.ValueObjects;
using RideSharing.Domain.Entities;

namespace RideSharing.Domain.Factories
{
	public class DriverFactory
	{
		public static Result<DriverEntity> Create(long id, long userId, string name, Gender gender, Email email, string phone, string location)
		{
			DriverEntity driver = new DriverEntity
			{
				Id = id,
				Name = name,
				Email = email.Value,
				Phone = phone,
				Location = location,
			};

			var validator = new DriverValidator();
			var validationResult = validator.Validate(driver);

			if (validationResult.IsValid) return Result.Success(driver);
			return Result.Failure<DriverEntity>("not valid");
		}

		private class DriverValidator : AbstractValidator<DriverEntity>
		{
			public DriverValidator()
			{
				RuleFor(c => c.Email).EmailAddress();
			}
		}
	}
}
