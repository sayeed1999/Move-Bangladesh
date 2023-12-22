using CSharpFunctionalExtensions;
using MediatR;
using RideSharing.Application.Abstractions;
using RideSharing.Domain;

namespace RideSharing.Application.TripUseCase.Commands.TripRequestCommand
{
    public class TripRequestCommandHandler
        : IRequestHandler<TripRequestCommandDto, Result<TripRequestCommandResponseDto>>
    {
        private readonly ITripRepository tripRepository;

        public TripRequestCommandHandler(ITripRepository tripRepository)
        {
            this.tripRepository = tripRepository;
        }

        public async Task<Result<TripRequestCommandResponseDto>> Handle(TripRequestCommandDto model, CancellationToken cancellationToken)
        {
            Result<Trip> trip = Trip.CreateNewTrip(model.CustomerId, model.DriverId, model.Source, model.Destination);
            if (trip.IsFailure) return Result.Failure<TripRequestCommandResponseDto>("Please provide valid data.");

            var res = await this.tripRepository.AddAsync(trip.Value);

            // TODO: use automapper to map into desired state
            return Result.Success<TripRequestCommandResponseDto>(new TripRequestCommandResponseDto());
        }
    }
}
