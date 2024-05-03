using CSharpFunctionalExtensions;
using FluentValidation;
using NetTopologySuite.Geometries;
using RideSharing.Domain.Entities;

namespace RideSharing.Domain.Factories
{
	public class TripRequestFactory
	{
		/// <summary>
		/// Use this method when a customer wants to request for a trip.
		/// </summary>
		/// <param name="customerId"></param>
		/// <param name="source"></param>
		/// <param name="destination"></param>
		/// <param name="cabType"></param>
		/// <param name="paymentMethod"></param>
		/// <returns></returns>
		public static Result<TripRequestEntity> Create(
		Guid customerId,
		Tuple<double,
		double> source,
		Tuple<double, double> destination,
		CabType cabType,
		PaymentMethod paymentMethod)
		{
			var x = new TripRequestEntity()
			{
				CustomerId = customerId,
				Source = new Point(source.Item1, source.Item2),
				Destination = new Point(destination.Item1, destination.Item2),
				CabType = cabType,
				PaymentMethod = paymentMethod,
				Status = TripRequestStatus.NoDriverAccepted, // This should be the default type if no driver accepts the ride.
				CreatedAt = DateTime.UtcNow,
				UpdatedAt = DateTime.UtcNow, // considering CreatedAt == UpdatedAt when trip is requested
			};

			var validator = new TripRequestValidator();
			var result = validator.Validate(x);

			if (result.IsValid) return Result.Success(x);
			return Result.Failure<TripRequestEntity>("Model is invalid");
		}

		private class TripRequestValidator : AbstractValidator<TripRequestEntity>
		{
			public TripRequestValidator()
			{
				RuleFor(x => x.Source).NotEmpty();
				RuleFor(x => x.Destination).NotEmpty();
			}
		}

	}
}
