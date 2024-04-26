using RideSharing.Application.Abstractions;
using RideSharing.Domain.Entities;

namespace RideSharing.InternalAPI.Controllers
{
	public class CustomerController : BaseController<CustomerEntity>
	{
		public CustomerController(ICustomerRepository repository) : base(repository)
		{

		}
	}
}
