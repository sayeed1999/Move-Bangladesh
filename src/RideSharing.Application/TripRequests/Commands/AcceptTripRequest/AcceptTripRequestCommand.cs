using CSharpFunctionalExtensions;
using MediatR;

namespace RideSharing.Application.TripRequests.Commands.AcceptTripRequest
{
	public record struct AcceptTripRequestCommand(
		string DriverId,
		string TripRequestId) : IRequest<Result<string>>
	{
	}
}
