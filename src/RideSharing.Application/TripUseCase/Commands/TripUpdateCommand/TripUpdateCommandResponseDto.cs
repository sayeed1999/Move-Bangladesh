using NetTopologySuite.Geometries;
using RideSharing.Domain.Entities;

namespace RideSharing.Application.TripUseCase.Commands.TripUpdateCommand
{
	public record struct TripUpdateCommandResponseDto(
		Guid TripId,
		Guid CustomerId,
		Guid DriverId,
		Point Source,
		Point Destination,
		string TripStatus)
	{
		public TripUpdateCommandResponseDto(Trip trip)
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
