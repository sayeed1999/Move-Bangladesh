using CSharpFunctionalExtensions;
using MediatR;

namespace RideSharing.Application.TripUseCase.Queries.TripStatusQuery
{
    public class TripStatusQuery
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
