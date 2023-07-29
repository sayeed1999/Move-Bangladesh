using RideSharing.Entity;
using Sayeed.Generic.OnionArchitecture.Logic;
using Sayeed.Generic.OnionArchitecture.Repository;
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
