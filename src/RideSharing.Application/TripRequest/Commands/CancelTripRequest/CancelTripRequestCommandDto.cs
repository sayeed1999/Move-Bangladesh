using CSharpFunctionalExtensions;
using MediatR;

namespace RideSharing.Application.TripRequest.Commands.CancelTripRequest
{
	public record struct CancelTripRequestCommandDto(
		long CustomerId,
		long TripRequestId)
		: IRequest<Result<long>>
	{

	}
}
