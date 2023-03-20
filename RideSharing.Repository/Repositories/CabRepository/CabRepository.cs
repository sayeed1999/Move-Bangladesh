using RideSharing.Entity;
using RideSharing.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharing.Repository
{
    public class CabRepository : BaseRepository<Cab>, ICabRepository
    {
        public CabRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
        }
    }
}
