using CSharpFunctionalExtensions;
using MediatR;

namespace RideSharing.Application.TripRequest.Commands.RejectByCustomer
{
	public record struct RejectByCustomerCommandDto(
		Guid CustomerId,
		Guid TripId,
		string Reason) : IRequest<Result<Guid>>
	{

	}
}
