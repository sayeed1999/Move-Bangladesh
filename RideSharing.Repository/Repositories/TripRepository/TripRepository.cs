using RideSharing.Entity;
using RideSharing.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharing.Repository
{
    public class TripRepository : BaseRepository<Trip>, ITripRepository
    {
        public TripRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
