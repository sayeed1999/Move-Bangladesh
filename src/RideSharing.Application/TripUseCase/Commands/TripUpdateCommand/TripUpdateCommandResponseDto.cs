using RideSharing.Domain.Entities;

namespace RideSharing.Application.TripUseCase.Commands.TripUpdateCommand
{
	public class TripUpdateCommandResponseDto
	{
		public TripUpdateCommandResponseDto(Trip trip)
		{
			TripId = trip.Id;
			CustomerId = trip.CustomerId;
			DriverId = trip.DriverId;
			Source = trip.Source;
			Destination = trip.Destination;
			TripStatus = trip.TripStatus.ToString();
		}

		public long TripId { get; set; }
		public long CustomerId { get; set; }
		public long DriverId { get; set; }
		public string Source { get; set; }
		public string Destination { get; set; }
		public string TripStatus { get; set; }
	}
}
