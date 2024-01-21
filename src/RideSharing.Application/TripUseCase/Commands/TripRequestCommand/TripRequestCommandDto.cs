using CSharpFunctionalExtensions;
using MediatR;
using RideSharing.Domain.Enums;

namespace RideSharing.Application.TripUseCase.Commands.TripRequestCommand
{
	public record struct TripRequestCommandDto(
		Guid CustomerId,
		Tuple<double, double> Source,
		Tuple<double, double> Destination,
		CabType CabType)
		: IRequest<Result<TripRequestCommandResponseDto>>
	{

	}
}
