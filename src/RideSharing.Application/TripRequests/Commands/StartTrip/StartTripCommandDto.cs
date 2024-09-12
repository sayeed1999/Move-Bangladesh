using CSharpFunctionalExtensions;
using MediatR;

namespace RideSharing.Application.TripRequests.Commands.StartTrip
{
	public record struct StartTripCommandDto(
		string DriverId,
		string TripRequestId) : IRequest<Result<string>>
	{

	}
}
