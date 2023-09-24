using CSharpFunctionalExtensions;
using MediatR;
using RideSharing.Entity;
using RideSharing.Entity.Dtos;
using Sayeed.Generic.OnionArchitecture.Logic;
using Sayeed.Generic.OnionArchitecture.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharing.Service
{
    public class TripService : BaseService<Trip>, ITripService, 
        IRequestHandler<TripRequestDto, Result<Trip>>,
        IRequestHandler<TripModifyDto, Result<Trip>>,
        IRequestHandler<TripQueryDto, Result<Trip>>
    {
        private readonly IBaseRepository<Trip> baseRepository;

        public TripService(IBaseRepository<Trip> baseRepository) : base(baseRepository)
        {
            this.baseRepository = baseRepository;
        }

        public async Task<Result<Trip>> Handle(TripRequestDto model, CancellationToken cancellationToken)
        {
            Result<Trip> trip = Trip.CreateNewTrip(model.CustomerId, model.DriverId, model.Source, model.Destination);
            if (trip.IsFailure) return Result.Failure<Trip>("Please provide valid data.");

            var res = await this.baseRepository.AddAsync(trip.Value);

            return Result.Success<Trip>(res);
        }

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

        public async Task<Result<Trip>> Handle(TripQueryDto model, CancellationToken cancellationToken)
        {
            var tripInDB = await this.baseRepository.FindByIdAsync(model.TripId);
            if (tripInDB == null) return Result.Failure<Trip>($"Ride request {model.TripId} not found.");

            return Result.Success<Trip>(tripInDB);
        }
    }
}