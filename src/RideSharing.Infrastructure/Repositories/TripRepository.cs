using RideSharing.Application.Abstractions;
using RideSharing.Domain;
using RideSharing.Infrastructure;
using Sayeed.Generic.OnionArchitecture.Repository;

namespace RideSharing.Application
{
    public partial class TripRepository : BaseRepository<Trip>, ITripRepository
    {
        public TripRepository(ApplicationDbContext applicationDbContext) : base(applicationDbContext)
        {

        }
    }
}