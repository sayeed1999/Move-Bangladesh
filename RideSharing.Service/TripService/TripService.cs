using RideSharing.Entity;
using RideSharing.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharing.Repository
{
    public class TripService : BaseService<Trip>, ITripService
    {
        public TripService(ITripRepository baseRepository) : base(baseRepository)
        {
        }
    }
}
