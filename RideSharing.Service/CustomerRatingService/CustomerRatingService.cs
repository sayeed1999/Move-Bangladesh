using RideSharing.Entity;
using Sayeed.NTier.Generic.Repository;

namespace RideSharing.Service
{
    public class CustomerRatingService : BaseService<CustomerRating>, ICustomerRatingService
    {
        public CustomerRatingService(IBaseRepository<CustomerRating> repository) : base(repository)
        {
        }
    }
}