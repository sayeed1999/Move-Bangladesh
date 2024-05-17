using CSharpFunctionalExtensions;
using MediatR;
using RideSharing.Domain.Entities;

namespace RideSharing.Application.TripRequest.Commands.TripRequest
{
	public record struct TripRequestCommandDto(
		long CustomerId,
		Tuple<double, double> Source,
		Tuple<double, double> Destination,
		CabType CabType,
		PaymentMethod PaymentMethod)
		: IRequest<Result<long>>
	{

	}
}
