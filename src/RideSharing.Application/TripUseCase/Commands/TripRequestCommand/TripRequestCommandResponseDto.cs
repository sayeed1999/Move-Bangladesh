using NetTopologySuite.Geometries;
using RideSharing.Domain.Entities;

namespace RideSharing.Application.TripUseCase.Commands.TripRequestCommand
{
	public record struct TripRequestCommandResponseDto(
		Guid TripId,
		Guid CustomerId,
		Guid DriverId,
		Point Source,
		Point Destination,
		string TripStatus)
	{
		public TripRequestCommandResponseDto(Trip trip)
			: this(trip.Id,
				  trip.CustomerId,
				  trip.DriverId,
				  trip.Source,
				  trip.Destination,
				  trip.TripStatus.ToString())
		{

		}
	}
}
