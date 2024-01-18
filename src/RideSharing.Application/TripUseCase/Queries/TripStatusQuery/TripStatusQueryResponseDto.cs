using RideSharing.Domain.Entities;

namespace RideSharing.Application.TripUseCase.Queries.TripStatusQuery
{
	public record struct TripStatusQueryResponseDto(
		Guid TripId,
		Guid CustomerId,
		Guid DriverId,
		Tuple<double, double> Source,
		Tuple<double, double> Destination)
	{
		public TripStatusQueryResponseDto(Trip trip)
			: this(trip.Id,
				  trip.CustomerId,
				  trip.DriverId,
				  new Tuple<double, double>(trip.Source.X, trip.Source.Y),
				  new Tuple<double, double>(trip.Destination.X, trip.Destination.Y))
		{

		}
	}
}
