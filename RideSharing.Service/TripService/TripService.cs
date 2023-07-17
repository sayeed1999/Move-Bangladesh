using RideSharing.Entity;
using Sayeed.NTier.Generic.Logic;
using Sayeed.NTier.Generic.Repository;

namespace RideSharing.Service
{
    public class TripService : BaseService<Trip>, ITripService
    {
        public TripService(IBaseRepository<Trip> baseRepository) : base(baseRepository)
        {
        }
    }
}