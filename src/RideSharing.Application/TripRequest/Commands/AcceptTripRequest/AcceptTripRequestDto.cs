using CSharpFunctionalExtensions;
using MediatR;

namespace RideSharing.Application.TripRequest.Commands.AcceptTripRequest
{
	public record struct AcceptTripRequestDto(
		long DriverId,
		long TripRequestId) : IRequest<Result<long>>
	{
	}
}
