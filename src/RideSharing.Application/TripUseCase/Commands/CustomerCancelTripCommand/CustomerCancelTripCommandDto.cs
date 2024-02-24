using CSharpFunctionalExtensions;
using MediatR;

namespace RideSharing.Application.TripUseCase.Commands.CustomerCancelTripCommand
{
	public record struct CustomerCancelTripCommandDto(
		Guid CustomerId,
		Guid TripId,
		string Reason) : IRequest<Result<CustomerCancelTripCommandResponseDto>>
	{

	}
}
