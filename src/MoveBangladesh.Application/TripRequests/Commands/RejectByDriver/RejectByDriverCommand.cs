using CSharpFunctionalExtensions;
using MediatR;

namespace MoveBangladesh.Application.TripRequests.Commands.RejectByDriver
{
	public record struct RejectByDriverCommand(
		string DriverId,
		string TripRequestId,
		string Reason) : IRequest<Result<string>>
	{

	}
}
