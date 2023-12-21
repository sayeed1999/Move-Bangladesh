using CSharpFunctionalExtensions;
using MediatR;

namespace RideSharing.Application.TripUseCase.Queries.TripStatusQuery
{
    public class TripStatusQueryDto : IRequest<Result<TripStatusQueryResponseDto>>
    {
        private TripStatusQueryDto() { }

        public static TripStatusQueryDto Create(int tripId)
        {
            return new TripStatusQueryDto()
            {
                TripId = tripId,
            };
        }

        public long TripId { get; private set; }
    }
}
