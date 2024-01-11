using CSharpFunctionalExtensions;
using MediatR;
using RideSharing.Domain.Enums;

namespace RideSharing.Application.TripUseCase.Commands.TripUpdateCommand
{
	public record struct TripUpdateCommandDto(
		Guid TripId,
		TripStatus TripStatus) : IRequest<Result<TripUpdateCommandResponseDto>>
	{

	}
}
