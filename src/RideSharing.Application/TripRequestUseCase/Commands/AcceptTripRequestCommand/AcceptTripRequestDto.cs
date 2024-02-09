using CSharpFunctionalExtensions;
using MediatR;

namespace RideSharing.Application.TripRequestUseCase.Commands.AcceptTripRequestCommand
{
	public record struct AcceptTripRequestDto(
		Guid DriverId,
		Guid TripRequestId) : IRequest<Result<AcceptTripRequestResponseDto>>
	{
	}
}
