using RideSharing.Entity;
using Sayeed.NTier.Generic.Repository;

namespace RideSharing.Service
{
    public class CabService : BaseService<Cab>, ICabService
    {
        public CabService(IBaseRepository<Cab> repository) : base(repository)
        {
        }
    }
}