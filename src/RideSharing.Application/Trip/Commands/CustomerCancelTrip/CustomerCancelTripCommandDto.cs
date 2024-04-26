using CSharpFunctionalExtensions;
using MediatR;

namespace RideSharing.Application.Trip.Commands.CustomerCancelTrip
{
	public record struct CustomerCancelTripCommandDto(
		Guid CustomerId,
		Guid TripId,
		string Reason) : IRequest<Result<Guid>>
	{

	}
}
