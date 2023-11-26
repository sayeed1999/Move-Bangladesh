using CSharpFunctionalExtensions;
using MediatR;
using RideSharing.Entity.Dtos;
using RideSharing.Entity;

namespace RideSharing.Service
{
    public partial class TripService
        : IRequestHandler<TripQueryDto, Result<Trip>>
    {
        public async Task<Result<Trip>> Handle(TripQueryDto model, CancellationToken cancellationToken)
        {
            var tripInDB = await this.baseRepository.FindByIdAsync(model.TripId);
            if (tripInDB == null) return Result.Failure<Trip>($"Ride request {model.TripId} not found.");

            return Result.Success<Trip>(tripInDB);
        }

    }
}
