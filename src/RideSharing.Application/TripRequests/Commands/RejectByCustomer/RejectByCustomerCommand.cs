using CSharpFunctionalExtensions;
using MediatR;

namespace RideSharing.Application.TripRequests.Commands.RejectByCustomer
{
	public record struct RejectByCustomerCommand(
		string CustomerId,
		string TripRequestId,
		string Reason) : IRequest<Result<string>>
	{

	}
}
