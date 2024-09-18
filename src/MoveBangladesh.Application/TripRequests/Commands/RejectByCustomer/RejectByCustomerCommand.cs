using CSharpFunctionalExtensions;
using MediatR;

namespace MoveBangladesh.Application.TripRequests.Commands.RejectByCustomer
{
	public record struct RejectByCustomerCommand(
		string CustomerId,
		string TripRequestId,
		string Reason) : IRequest<Result<string>>
	{

	}
}
