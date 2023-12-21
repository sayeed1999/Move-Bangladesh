using CSharpFunctionalExtensions;
using MediatR;
using RideSharing.Domain;

namespace RideSharing.Application.TripHandlers.Commands.TripRequestCommand
{
    public class TripRequestCommandDto : IRequest<Result<Trip>>
    {
        public long CustomerId { get; set; }
        public long DriverId { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
    }
}
