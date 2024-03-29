using CSharpFunctionalExtensions;
using FluentValidation;
using RideSharing.Domain.Entities;
using RideSharing.Domain.Enums;

namespace RideSharing.Domain.Factories
{
	public class TripFactory
	{
		public static Result<Trip> Create(TripRequest tripRequest, Guid driverId)
		{
			return new Trip()
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

		private class TripValidator : AbstractValidator<Trip>
		{
			public TripValidator()
			{
				RuleFor(x => x.Source).NotEmpty();
				RuleFor(x => x.Destination).NotEmpty();
			}
		}

	}
}
