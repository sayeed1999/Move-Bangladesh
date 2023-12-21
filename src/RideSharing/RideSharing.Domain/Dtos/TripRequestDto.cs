using CSharpFunctionalExtensions;
using MediatR;

namespace RideSharing.Domain.Dtos
{
    public class TripRequestDto : IRequest<Result<Trip>>
    {
        public long CustomerId { get; set; }
        public long DriverId { get; set; }
        public string Source { get; set; }
        public string Destination { get; set; }
    }
}
