using CSharpFunctionalExtensions;
using MediatR;

namespace RideSharing.Application.TripRequest.Commands.RejectByCustomer
{
	public record struct RejectByCustomerCommandDto(
		long CustomerId,
		long TripRequestId,
		string Reason) : IRequest<Result<long>>
	{

	}
}
