using CSharpFunctionalExtensions;
using MediatR;

namespace RideSharing.Application.TripRequest.Commands.RejectByDriver
{
	public record struct RejectByDriverCommandDto(
		long DriverId,
		long TripRequestId,
		string Reason) : IRequest<Result<long>>
	{

	}
}
