using CSharpFunctionalExtensions;
using MediatR;

namespace RideSharing.Application.TripRequests.Commands.RejectByDriver
{
	public record struct RejectByDriverCommandDto(
		string DriverId,
		string TripRequestId,
		string Reason) : IRequest<Result<string>>
	{

	}
}
