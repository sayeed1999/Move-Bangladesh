using CSharpFunctionalExtensions;
using MediatR;

namespace RideSharing.Application.TripUseCase.Commands.CancelTripRequestCommand
{
	public record struct CancelTripRequestCommandDto(
		Guid CustomerId,
		Guid TripRequestId)
		: IRequest<Result<CancelTripRequestCommandResponseDto>>
	{

	}
}
