using CSharpFunctionalExtensions;
using MediatR;
using RideSharing.Application.Abstractions;
using RideSharing.Domain;

namespace RideSharing.Application.TripUseCase.Commands.TripUpdateCommand
{
    public class TripUpdateCommandHandler
        : IRequestHandler<TripUpdateCommandDto, Result<TripUpdateCommandResponseDto>>
    {
        private readonly ITripRepository tripRepository;

        public TripUpdateCommandHandler(ITripRepository tripRepository)
        {
            this.tripRepository = tripRepository;
        }

        public async Task<Result<TripUpdateCommandResponseDto>> Handle(TripUpdateCommandDto model, CancellationToken cancellationToken)
        {
            var tripInDB = await this.tripRepository.FindByIdAsync(model.TripId);
            if (tripInDB == null) return Result.Failure<TripUpdateCommandResponseDto>($"Ride request {model.TripId} not found.");

            // Logic: A Trip Status can only update incrementally. Check TripStatus enum.
            if (tripInDB.Status >= model.TripStatus) return Result.Failure<TripUpdateCommandResponseDto>("Cannot reverse a trip status to a past value!");

            var trip = Trip.Modify(model.TripId, model.TripStatus);

            var res = await this.tripRepository.UpdateAsync(trip.Value);

            // TODO: use automapper to map into desired state
            return Result.Success<TripUpdateCommandResponseDto>(new TripUpdateCommandResponseDto());
        }
    }
}
