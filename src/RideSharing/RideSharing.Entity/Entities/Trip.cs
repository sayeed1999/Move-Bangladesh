using CSharpFunctionalExtensions;
using FluentValidation;
using RideSharing.Domain;
using RideSharing.Domain.Enums;
using Sayeed.Generic.OnionArchitecture.Entity;

namespace RideSharing.Domain
{
    public class Trip : BaseEntity
    {
        private Trip() : base() { }
        private Trip(long customerId, Customer customer, long driverId, Driver driver, long paymentId, Payment payment, TripStatus status, string source, string destination) : base()
        {
            CustomerId = customerId;
            Customer = customer;
            DriverId = driverId;
            Driver = driver;
            PaymentId = paymentId;
            Payment = payment;
            Status = status;
            Source = source;
            Destination = destination;
        }

        public long CustomerId { get; protected set; }
        public virtual Customer Customer { get; protected set; }
        public long DriverId { get; protected set; }
        public virtual Driver Driver { get; protected set; }
        public long PaymentId { get; protected set; }
        public virtual Payment Payment { get; protected set; }
        public TripStatus Status { get; protected set; }
        public String Source { get; protected set; }
        public String Destination { get; protected set; }

        public static Result<Trip> CreateNewTrip(long customerId, long driverId, string source, string destination)
        {
            var x = new Trip()
            {
                Id = 0,
                CustomerId = customerId,
                DriverId = driverId,
                Source = source,
                Destination = destination,
                Status = TripStatus.TripRequested,
            };

            var validator = new TripValidator();
            var result = validator.Validate(x);

            if (result.IsValid) return Result.Success(x);
            return Result.Failure<Trip>("Model is invalid");
        }

        public static Result<Trip> Modify(long id, TripStatus status)
        {
            if (status == null || id <= 0) return Result.Failure<Trip>("Model is invalid");

            var x = new Trip()
            {
                Id = id,
                Status = status,
            };

            return Result.Success(x);
        }

    }
}

public class TripValidator : AbstractValidator<Trip>
{
    public TripValidator()
    {
        RuleFor(x => x.Id).GreaterThanOrEqualTo(0);
        RuleFor(x => x.CustomerId).GreaterThan(0);
        RuleFor(x => x.DriverId).GreaterThan(0);
        RuleFor(x => x.Source).NotEmpty();
        RuleFor(x => x.Destination).NotEmpty();
    }
}
