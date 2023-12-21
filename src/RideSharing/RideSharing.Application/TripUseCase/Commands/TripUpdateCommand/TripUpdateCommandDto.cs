using CSharpFunctionalExtensions;
using MediatR;
using RideSharing.Domain;
using RideSharing.Domain.Enums;

namespace RideSharing.Application.TripHandlers.Commands.TripUpdateCommand
{
    public class TripUpdateCommandDto : IRequest<Result<Trip>>
    {
        public long TripId { get; set; }
        public TripStatus TripStatus { get; set; }
    }
}
