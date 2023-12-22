using CSharpFunctionalExtensions;
using MediatR;
using RideSharing.Domain.Enums;

namespace RideSharing.Application.TripUseCase.Commands.TripUpdateCommand
{
    public class TripUpdateCommandDto : IRequest<Result<TripUpdateCommandResponseDto>>
    {
        public long TripId { get; set; }
        public TripStatus TripStatus { get; set; }
    }
}
