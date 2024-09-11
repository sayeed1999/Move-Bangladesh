using CSharpFunctionalExtensions;
using MediatR;

namespace RideSharing.Application.TripRequest.Commands.CancelTripRequest
{
	public record struct CancelTripRequestCommandDto(
		string CustomerId,
		string TripRequestId)
		: IRequest<Result<string>>
	{

	}
}
