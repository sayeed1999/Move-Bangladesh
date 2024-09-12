using RideSharing.Application.Abstractions;
using RideSharing.Domain.Entities;

namespace RideSharing.CustomerAPI.Controllers
{
	public class CustomerController : BaseController<Customer>
	{
		public CustomerController(IBaseRepository<Customer> repository) : base(repository)
		{

		}
	}
}
