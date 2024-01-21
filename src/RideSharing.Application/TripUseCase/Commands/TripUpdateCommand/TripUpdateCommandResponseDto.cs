using RideSharing.Domain.Entities;

namespace RideSharing.Application.TripUseCase.Commands.TripUpdateCommand
{
	public record struct TripUpdateCommandResponseDto(
		Guid TripId,
		Guid CustomerId,
		Guid? DriverId,
		Tuple<double, double> Source,
		Tuple<double, double> Destination,
		string TripStatus)
	{
		public TripUpdateCommandResponseDto(Trip trip)
			: this(trip.Id,
				  trip.CustomerId,
				  trip.DriverId,
				  new Tuple<double, double>(trip.Source.X, trip.Source.Y),
				  new Tuple<double, double>(trip.Destination.X, trip.Destination.Y),
				  trip.TripStatus.ToString())
		{

		}
	}
}
