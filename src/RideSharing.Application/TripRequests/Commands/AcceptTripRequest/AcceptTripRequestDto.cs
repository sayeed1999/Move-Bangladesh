using CSharpFunctionalExtensions;
using MediatR;

namespace RideSharing.Application.TripRequests.Commands.AcceptTripRequest
{
	public record struct AcceptTripRequestDto(
		string DriverId,
		string TripRequestId) : IRequest<Result<string>>
	{
	}
}
