using CSharpFunctionalExtensions;
using MediatR;

namespace RideSharing.Application.TripRequest.Commands.RejectByCustomer
{
	public record struct RejectByCustomerCommandDto(
		string CustomerId,
		string TripRequestId,
		string Reason) : IRequest<Result<string>>
	{

	}
}
