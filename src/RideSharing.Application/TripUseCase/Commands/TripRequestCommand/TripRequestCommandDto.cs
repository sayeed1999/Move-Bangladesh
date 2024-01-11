using CSharpFunctionalExtensions;
using MediatR;
using NetTopologySuite.Geometries;

namespace RideSharing.Application.TripUseCase.Commands.TripRequestCommand
{
	public record struct TripRequestCommandDto(
		Guid CustomerId,
		Guid DriverId,
		Point Source,
		Point Destination)
		: IRequest<Result<TripRequestCommandResponseDto>>
	{

	}
}
