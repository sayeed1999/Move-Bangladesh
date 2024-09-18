using CSharpFunctionalExtensions;
using MediatR;

namespace MoveBangladesh.Application.TripRequests.Commands.AcceptTripRequest
{
	public record struct AcceptTripRequestCommand(
		string DriverId,
		string TripRequestId) : IRequest<Result<string>>
	{
	}
}
