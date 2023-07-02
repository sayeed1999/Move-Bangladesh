using RideSharing.Entity;
using Sayeed.NTier.Generic.Logic;
using Sayeed.NTier.Generic.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharing.Service
{
    public class CustomerRatingService : BaseService<CustomerRating>, ICustomerRatingService
    {
        public CustomerRatingService(IBaseRepository<CustomerRating> repository) : base(repository)
        {
        }
    }
}
