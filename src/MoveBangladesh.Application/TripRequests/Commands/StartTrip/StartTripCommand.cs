using CSharpFunctionalExtensions;
using MediatR;

namespace MoveBangladesh.Application.TripRequests.Commands.StartTrip
{
	public record struct StartTripCommand(
		string DriverId,
		string TripRequestId) : IRequest<Result<string>>
	{

	}
}
