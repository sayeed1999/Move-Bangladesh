using CSharpFunctionalExtensions;
using MediatR;
using RideSharing.Domain.Enums;

namespace RideSharing.Domain.Dtos
{
    public class TripModifyDto : IRequest<Result<Trip>>
    {
        public long TripId { get; set; }
        public TripStatus TripStatus { get; set; }
    }
}
