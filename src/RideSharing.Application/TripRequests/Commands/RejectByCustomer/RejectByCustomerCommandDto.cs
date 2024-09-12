using CSharpFunctionalExtensions;
using MediatR;

namespace RideSharing.Application.TripRequests.Commands.RejectByCustomer
{
	public record struct RejectByCustomerCommandDto(
		string CustomerId,
		string TripRequestId,
		string Reason) : IRequest<Result<string>>
	{

	}
}
