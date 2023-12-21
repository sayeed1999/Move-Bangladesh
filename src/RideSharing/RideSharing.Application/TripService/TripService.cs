using CSharpFunctionalExtensions;
using MediatR;
using RideSharing.Domain;
using RideSharing.Domain.Dtos;
using Sayeed.Generic.OnionArchitecture.Logic;
using Sayeed.Generic.OnionArchitecture.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharing.Application
{
    public partial class TripService : BaseService<Trip>, ITripService
    {
        private readonly IBaseRepository<Trip> baseRepository;

        public TripService(IBaseRepository<Trip> baseRepository) : base(baseRepository)
        {
            this.baseRepository = baseRepository;
        }
    }
}