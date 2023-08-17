using CSharpFunctionalExtensions;
using FluentValidation;
using RideSharing.Common.Enums;
using RideSharing.Entity;

namespace RideSharing.Entity
{
    public class Driver : Human
    {
        private Driver(long id, string firstName, string lastName, Gender gender, string email, string userName, string phoneNumber) : base()
        {
            Id = id;
            Name = firstName + " " + lastName;
            Gender = gender;
            Email = email;
            Phone = phoneNumber;

            CustomerRatings = new();
            Trips = new();
        }
        public virtual List<CustomerRating> CustomerRatings { get; protected set; }
        public virtual List<Trip> Trips { get; protected set; }

        public static Result<Driver> Create(long id, string firstName, string lastName, Gender gender, string email, string userName, string phoneNumber)
        {
            var driver = new Driver(id, firstName, lastName, gender, email, userName,phoneNumber);

            var validator = new DriverValidator();
            var r = validator.Validate(driver);

            if (r.IsValid) return Result.Success(driver);
            return Result.Failure<Driver>("domain not valid");
        }
    }
}
public class DriverValidator : AbstractValidator<Driver>
{
    public DriverValidator()
    {
        RuleFor(c => c.Id).GreaterThanOrEqualTo(0);
        RuleFor(c => c.Email).EmailAddress();
    }
}
