using CSharpFunctionalExtensions;
using MediatR;
using RideSharing.Domain.Entities;

namespace RideSharing.Application.TripRequests.Commands.TripRequests
{
	public record struct RequestTripCommand(
		string CustomerId,
		Tuple<float, float> Source,
		Tuple<float, float> Destination,
		CabType CabType,
		PaymentMethod PaymentMethod)
		: IRequest<Result<string>>
	{

	}
}
