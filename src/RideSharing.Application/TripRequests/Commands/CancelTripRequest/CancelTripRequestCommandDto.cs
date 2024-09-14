using CSharpFunctionalExtensions;
using MediatR;

namespace RideSharing.Application.TripRequests.Commands.CancelTripRequest
{
	public record struct CancelRequestTripCommandDto(
		string CustomerId,
		string TripRequestId)
		: IRequest<Result<string>>
	{

	}
}
