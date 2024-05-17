using CSharpFunctionalExtensions;
using MediatR;

namespace RideSharing.Application.TripRequest.Commands.RejectByDriver
{
	public record struct RejectByDriverCommandDto(
		long DriverId,
		long TripId,
		string Reason) : IRequest<Result<long>>
	{

	}
}
