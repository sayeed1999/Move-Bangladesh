using CSharpFunctionalExtensions;
using MediatR;

namespace RideSharing.Application.TripRequest.Commands.RejectByDriver
{
	public record struct RejectByDriverCommandDto(
		string DriverId,
		string TripRequestId,
		string Reason) : IRequest<Result<string>>
	{

	}
}
