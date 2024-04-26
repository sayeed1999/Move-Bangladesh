using CSharpFunctionalExtensions;
using MediatR;

namespace RideSharing.Application.TripRequest.Commands.CancelTripRequest
{
	public record struct CancelTripRequestCommandDto(
		Guid CustomerId,
		Guid TripRequestId)
		: IRequest<Result<Guid>>
	{

	}
}
