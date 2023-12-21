using CSharpFunctionalExtensions;
using MediatR;
using RideSharing.Application.TripHandlers.Commands.TripUpdateCommand;
using RideSharing.Domain;

namespace RideSharing.Application
{
    public partial class TripService
        : IRequestHandler<TripUpdateCommandDto, Result<TripUpdateCommandResponseDto>>
    {
        public async Task<Result<TripUpdateCommandResponseDto>> Handle(TripUpdateCommandDto model, CancellationToken cancellationToken)
        {
            var tripInDB = await this.baseRepository.FindByIdAsync(model.TripId);
            if (tripInDB == null) return Result.Failure<TripUpdateCommandResponseDto>($"Ride request {model.TripId} not found.");

            // Logic: A Trip Status can only update incrementally. Check TripStatus enum.
            if (tripInDB.Status >= model.TripStatus) return Result.Failure<Trip>("Cannot reverse a trip status to a past value!");

            var trip = Trip.Modify(model.TripId, model.TripStatus);

            var res = await this.baseRepository.UpdateAsync(trip.Value);

            return Result.Success<TripUpdateCommandResponseDto>(res);
        }
    }
}
