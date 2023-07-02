using RideSharing.Entity;
using Sayeed.NTier.Generic.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RideSharing.Service
{
    public class CustomerService : BaseService<Customer>, ICustomerService
    {
        public CustomerService(IBaseRepository<Customer> repository) : base(repository)
        {
        }
    }
}
