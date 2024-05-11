using CSharpFunctionalExtensions;
using MediatR;

namespace RideSharing.Application.TripRequest.Commands.RejectByDriver
{
	public record struct RejectByDriverCommandDto(
		Guid DriverId,
		Guid TripId,
		string Reason) : IRequest<Result<Guid>>
	{

	}
}
