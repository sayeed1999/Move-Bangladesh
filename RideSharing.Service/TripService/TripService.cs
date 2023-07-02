using RideSharing.Entity;
using Sayeed.NTier.Generic.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharing.Service
{
    public class TripService : BaseService<Trip>, ITripService
    {
        public TripService(IBaseRepository<Trip> baseRepository) : base(baseRepository)
        {
        }
    }
}
