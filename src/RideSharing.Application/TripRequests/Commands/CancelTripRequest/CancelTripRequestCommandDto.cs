using CSharpFunctionalExtensions;
using MediatR;

namespace RideSharing.Application.TripRequests.Commands.CancelTripRequest
{
	public record struct CancelTripRequestCommandDto(
		string CustomerId,
		string TripRequestId)
		: IRequest<Result<string>>
	{

	}
}
