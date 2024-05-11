using CSharpFunctionalExtensions;
using MediatR;

namespace RideSharing.Application.Trip.Commands.CustomerCancelTrip
{
	public record struct RejectByCustomerCommandDto(
		Guid CustomerId,
		Guid TripId,
		string Reason) : IRequest<Result<Guid>>
	{

	}
}
