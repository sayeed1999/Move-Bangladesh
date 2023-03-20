using RideSharing.Entity;
using RideSharing.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharing.Service
{
    public class CustomerRatingService : BaseService<CustomerRating>, ICustomerRatingService
    {
        public CustomerRatingService(IBaseRepository<CustomerRating> baseRepository) : base(baseRepository)
        {
        }
    }
}
