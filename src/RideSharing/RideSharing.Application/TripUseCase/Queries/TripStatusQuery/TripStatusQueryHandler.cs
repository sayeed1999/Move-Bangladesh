using CSharpFunctionalExtensions;
using MediatR;
using RideSharing.Application.TripHandlers.Queries.TripStatusQuery;

namespace RideSharing.Application
{
    public partial class TripService
        : IRequestHandler<TripStatusQueryDto, Result<TripStatusQueryResponseDto>>
    {
        public async Task<Result<TripStatusQueryResponseDto>> Handle(TripStatusQueryDto model, CancellationToken cancellationToken)
        {
            var tripInDB = await this.baseRepository.FindByIdAsync(model.TripId);
            if (tripInDB == null) return Result.Failure<TripStatusQueryResponseDto>($"Ride request {model.TripId} not found.");

            return Result.Success<TripStatusQueryResponseDto>(tripInDB);
        }

    }
}
