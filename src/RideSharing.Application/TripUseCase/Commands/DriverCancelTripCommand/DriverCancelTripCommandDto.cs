using CSharpFunctionalExtensions;
using MediatR;

namespace RideSharing.Application.TripUseCase.Commands.DriverCancelTripCommand
{
	public record struct DriverCancelTripCommandDto(
		Guid DriverId,
		Guid TripId,
		string Reason) : IRequest<Result<DriverCancelTripCommandResponseDto>>
	{

	}
}
