using RideSharing.Entity;
using Sayeed.NTier.Generic.Repository;

namespace RideSharing.Service
{
    public class CustomerService : BaseService<Customer>, ICustomerService
    {
        public CustomerService(IBaseRepository<Customer> repository) : base(repository)
        {
        }
    }
}