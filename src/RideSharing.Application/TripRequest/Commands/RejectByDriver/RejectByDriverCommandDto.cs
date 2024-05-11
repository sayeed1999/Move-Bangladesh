using CSharpFunctionalExtensions;
using MediatR;

namespace RideSharing.Application.TripRequest.Commands.DriverCancelTrip
{
	public record struct RejectByDriverCommandDto(
		Guid DriverId,
		Guid TripId,
		string Reason) : IRequest<Result<Guid>>
	{

	}
}
