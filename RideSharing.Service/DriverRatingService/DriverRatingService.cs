using RideSharing.Entity;
using Sayeed.NTier.Generic.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharing.Service
{
    public class DriverRatingService : Sayeed.NTier.Generic.Logic.BaseService<DriverRating>, IDriverRatingService
    {
        public DriverRatingService(IBaseRepository<DriverRating> repository) : base(repository)
        {
        }
    }
}
