using CSharpFunctionalExtensions;
using FluentValidation;
using RideSharing.Domain.Entities;

namespace RideSharing.Domain.Factories
{
	public class TripFactory
	{
		public static Result<TripEntity> Create(TripRequestEntity tripRequest, Guid driverId)
		{
			return new TripEntity()
			{
				TripRequestId = tripRequest.Id,
				CustomerId = tripRequest.CustomerId,
				DriverId = driverId,
				PaymentMethod = tripRequest.PaymentMethod,
				TripStatus = TripStatus.DriverAccepted,
				Source = tripRequest.Source,
				Destination = tripRequest.Destination,
				CabType = tripRequest.CabType,
			};
		}

		private class TripValidator : AbstractValidator<TripEntity>
		{
			public TripValidator()
			{
				RuleFor(x => x.Source).NotEmpty();
				RuleFor(x => x.Destination).NotEmpty();
			}
		}

	}
}
