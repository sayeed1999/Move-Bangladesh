using RideSharing.Domain.Entities;
using RideSharing.ServiceBus.Abstractions;

namespace RideSharing.Application.TripRequestUseCase.Commands.TripRequestCommand
{
	public record struct TripRequestCommandResponseDto(
		Guid Id,
		Guid CustomerId,
		Tuple<double, double> Source,
		Tuple<double, double> Destination,
		string TripStatus,
		string CabType,
		string PaymentMethod
		) : IIntegrationEvent
	{
		public TripRequestCommandResponseDto(TripRequest trip)
			: this(trip.Id,
				  trip.CustomerId,
				  new Tuple<double, double>(trip.Source.X, trip.Source.Y),
				  new Tuple<double, double>(trip.Destination.X, trip.Destination.Y),
				  trip.Status.ToString(),
				  trip.CabType.ToString(),
				  trip.PaymentMethod.ToString())
		{

		}
	}
}
