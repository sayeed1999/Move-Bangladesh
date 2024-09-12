using CSharpFunctionalExtensions;
using MediatR;
using RideSharing.Domain.Entities;

namespace RideSharing.Application.TripRequests.Commands.TripRequests
{
	public record struct TripRequestCommandDto(
		string CustomerId,
		Tuple<double, double> Source,
		Tuple<double, double> Destination,
		CabType CabType,
		PaymentMethod PaymentMethod)
		: IRequest<Result<string>>
	{

	}
}
