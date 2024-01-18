using CSharpFunctionalExtensions;
using MediatR;

namespace RideSharing.Application.TripUseCase.Commands.TripRequestCommand
{
	public record struct TripRequestCommandDto(
		Guid CustomerId,
		Guid DriverId,
		Tuple<double, double> Source,
		Tuple<double, double> Destination)
		: IRequest<Result<TripRequestCommandResponseDto>>
	{

	}
}
