using CSharpFunctionalExtensions;
using MediatR;

namespace RideSharing.Application.Trip.Commands.DriverCancelTrip
{
	public record struct DriverCancelTripCommandDto(
		Guid DriverId,
		Guid TripId,
		string Reason) : IRequest<Result<Guid>>
	{

	}
}
