using CSharpFunctionalExtensions;
using MediatR;

namespace RideSharing.Application.TripRequest.Commands.StartTrip
{
	public record struct StartTripCommandDto(
		string DriverId,
		string TripRequestId) : IRequest<Result<string>>
	{

	}
}
