using RideSharing.Entity;
using Sayeed.NTier.Generic.Repository;

namespace RideSharing.Service
{
    public class DriverRatingService : BaseService<DriverRating>, IDriverRatingService
    {
        public DriverRatingService(IBaseRepository<DriverRating> repository) : base(repository)
        {
        }
    }
}