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
    public class TripService : BaseService<Trip>, ITripService, IRequestHandler<TripRequestDto, Result<Trip>>
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
    }
}