using CSharpFunctionalExtensions;
using MediatR;

namespace RideSharing.Application.Trip.Commands.StartTrip
{
	public record struct StartTripCommandDto(
		Guid DriverId,
		Guid TripRequestId) : IRequest<Result<Guid>>
	{

	}
}
