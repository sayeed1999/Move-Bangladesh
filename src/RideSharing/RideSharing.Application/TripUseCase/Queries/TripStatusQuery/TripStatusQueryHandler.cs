using CSharpFunctionalExtensions;
using MediatR;
using RideSharing.Application.Abstractions;

namespace RideSharing.Application.TripUseCase.Queries.TripStatusQuery
{
    public class TripStatusQueryHandler
        : IRequestHandler<TripStatusQueryDto, Result<TripStatusQueryResponseDto>>
    {
        private readonly ITripRepository tripRepository;

        public TripStatusQueryHandler(ITripRepository tripRepository)
        {
            this.tripRepository = tripRepository;
        }

        public async Task<Result<TripStatusQueryResponseDto>> Handle(TripStatusQueryDto model, CancellationToken cancellationToken)
        {
            var tripInDB = await this.tripRepository.FindByIdAsync(model.TripId);
            if (tripInDB == null) return Result.Failure<TripStatusQueryResponseDto>($"Ride request {model.TripId} not found.");

            // TODO: use automapper to map into desired state
            return Result.Success<TripStatusQueryResponseDto>(new TripStatusQueryResponseDto());
        }

    }
}
