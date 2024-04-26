using CSharpFunctionalExtensions;
using MediatR;
using RideSharing.Domain.Enums;

namespace RideSharing.Application.TripRequest.Commands.TripRequest
{
	public record struct TripRequestCommandDto(
		Guid CustomerId,
		Tuple<double, double> Source,
		Tuple<double, double> Destination,
		CabType CabType,
		PaymentMethod PaymentMethod)
		: IRequest<Result<Guid>>
	{

	}
}
