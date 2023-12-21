using CSharpFunctionalExtensions;
using MediatR;
using RideSharing.Application.TripHandlers.Commands.TripRequestCommand;
using RideSharing.Domain;

namespace RideSharing.Application
{
    public partial class TripService
        : IRequestHandler<TripRequestCommandDto, Result<TripRequestCommandResponseDto>>
    {
        public async Task<Result<TripRequestCommandResponseDto>> Handle(TripRequestCommandDto model, CancellationToken cancellationToken)
        {
            Result<Trip> trip = Trip.CreateNewTrip(model.CustomerId, model.DriverId, model.Source, model.Destination);
            if (trip.IsFailure) return Result.Failure<TripRequestCommandResponseDto>("Please provide valid data.");

            var res = await this.baseRepository.AddAsync(trip.Value);

            return Result.Success<TripRequestCommandResponseDto>(res);
        }
    }
}
