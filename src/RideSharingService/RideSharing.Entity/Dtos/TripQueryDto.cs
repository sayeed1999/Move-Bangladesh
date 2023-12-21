using CSharpFunctionalExtensions;
using MediatR;

namespace RideSharing.Domain.Dtos
{
    public class TripQueryDto : IRequest<Result<Trip>>
    {
        private TripQueryDto() { }

        public static TripQueryDto Create(int tripId)
        {
            return new TripQueryDto()
            {
                TripId = tripId,
            };
        }

        public long TripId { get; private set; }
    }
}
