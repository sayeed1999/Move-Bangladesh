using CSharpFunctionalExtensions;
using MediatR;

namespace RideSharing.Application.TripRequest.Commands.StartTrip
{
	public record struct StartTripCommandDto(
		long DriverId,
		long TripRequestId) : IRequest<Result<long>>
	{

	}
}
