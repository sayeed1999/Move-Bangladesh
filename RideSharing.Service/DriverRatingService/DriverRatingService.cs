using RideSharing.Entity;
using RideSharing.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharing.Service
{
    public class DriverRatingService : BaseService<DriverRating>, IDriverRatingService
    {
        public DriverRatingService(IBaseRepository<DriverRating> repository) : base(repository)
        {
        }
    }
}
