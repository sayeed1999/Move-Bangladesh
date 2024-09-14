using RideSharing.Application.Abstractions;
using RideSharing.Domain.Entities;

namespace RideSharing.CustomerAPI.Controllers
{
	public class CustomerAdminController : BaseAdminController<Customer>
	{
		public CustomerAdminController(IBaseRepository<Customer> repository) : base(repository)
		{

		}
	}
}
