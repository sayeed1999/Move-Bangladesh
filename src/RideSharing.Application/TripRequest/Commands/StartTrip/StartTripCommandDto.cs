using CSharpFunctionalExtensions;
using MediatR;

namespace RideSharing.Application.TripRequest.Commands.StartTrip
{
	public record struct StartTripCommandDto(
		Guid DriverId,
		Guid TripRequestId) : IRequest<Result<Guid>>
	{

	}
}
