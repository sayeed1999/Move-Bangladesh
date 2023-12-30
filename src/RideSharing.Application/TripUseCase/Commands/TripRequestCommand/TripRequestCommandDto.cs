using CSharpFunctionalExtensions;
using MediatR;
using NetTopologySuite.Geometries;

namespace RideSharing.Application.TripUseCase.Commands.TripRequestCommand
{
	public class TripRequestCommandDto : IRequest<Result<TripRequestCommandResponseDto>>
	{
		public Guid CustomerId { get; set; }
		public Guid DriverId { get; set; }
		public Point Source { get; set; }
		public Point Destination { get; set; }
	}
}
