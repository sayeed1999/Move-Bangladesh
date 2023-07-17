using RideSharing.Entity;
using Sayeed.NTier.Generic.Logic;
using Sayeed.NTier.Generic.Repository;

namespace RideSharing.Service
{
    public class DriverService : BaseService<Driver>, IDriverService
    {
        public DriverService(IBaseRepository<Driver> baseRepository) : base(baseRepository)
        {
        }
    }
}