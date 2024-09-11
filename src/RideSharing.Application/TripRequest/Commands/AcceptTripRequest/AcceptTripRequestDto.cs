using CSharpFunctionalExtensions;
using MediatR;

namespace RideSharing.Application.TripRequest.Commands.AcceptTripRequest
{
	public record struct AcceptTripRequestDto(
		string DriverId,
		string TripRequestId) : IRequest<Result<string>>
	{
	}
}
