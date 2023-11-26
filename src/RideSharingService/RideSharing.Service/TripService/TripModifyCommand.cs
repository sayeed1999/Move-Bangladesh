using CSharpFunctionalExtensions;
using MediatR;
using RideSharing.Entity.Dtos;
using RideSharing.Entity;

namespace RideSharing.Service
{
    public partial class TripService
        : IRequestHandler<TripModifyDto, Result<Trip>>
    {
        public async Task<Result<Trip>> Handle(TripModifyDto model, CancellationToken cancellationToken)
        {
            var tripInDB = await this.baseRepository.FindByIdAsync(model.TripId);
            if (tripInDB == null) return Result.Failure<Trip>($"Ride request {model.TripId} not found.");

            // Logic: A Trip Status can only update incrementally. Check TripStatus enum.
            if (tripInDB.Status >= model.TripStatus) return Result.Failure<Trip>("Cannot reverse a trip status to a past value!");

            var trip = Trip.Modify(model.TripId, model.TripStatus);

            var res = await this.baseRepository.UpdateAsync(trip.Value);

            return Result.Success<Trip>(res);
        }
    }
}
