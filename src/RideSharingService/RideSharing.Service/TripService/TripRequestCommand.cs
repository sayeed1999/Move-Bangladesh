using CSharpFunctionalExtensions;
using MediatR;
using RideSharing.Entity.Dtos;
using RideSharing.Entity;

namespace RideSharing.Service
{
    public partial class TripService
        : IRequestHandler<TripRequestDto, Result<Trip>>
    {
        public async Task<Result<Trip>> Handle(TripRequestDto model, CancellationToken cancellationToken)
        {
            Result<Trip> trip = Trip.CreateNewTrip(model.CustomerId, model.DriverId, model.Source, model.Destination);
            if (trip.IsFailure) return Result.Failure<Trip>("Please provide valid data.");

            var res = await this.baseRepository.AddAsync(trip.Value);

            return Result.Success<Trip>(res);
        }
    }
}
