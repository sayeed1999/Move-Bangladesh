using CSharpFunctionalExtensions;
using MediatR;

namespace RideSharing.Application.TripRequest.Commands.AcceptTripRequest
{
	public record struct AcceptTripRequestDto(
		Guid DriverId,
		Guid TripRequestId) : IRequest<Result<Guid>>
	{
	}
}
