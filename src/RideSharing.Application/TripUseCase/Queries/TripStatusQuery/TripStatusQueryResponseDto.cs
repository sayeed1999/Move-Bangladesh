using NetTopologySuite.Geometries;
using RideSharing.Domain.Entities;

namespace RideSharing.Application.TripUseCase.Queries.TripStatusQuery
{
	public record struct TripStatusQueryResponseDto(
		Guid TripId,
		Guid CustomerId,
		Guid DriverId,
		Point Source,
		Point Destination)
	{
		public TripStatusQueryResponseDto(Trip trip)
			: this(trip.Id,
				  trip.CustomerId,
				  trip.DriverId,
				  trip.Source,
				  trip.Destination)
		{

		}
	}
}
