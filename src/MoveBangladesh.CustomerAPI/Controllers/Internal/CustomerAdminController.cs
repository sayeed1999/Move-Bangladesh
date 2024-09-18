using MoveBangladesh.Application.Abstractions;
using MoveBangladesh.Domain.Entities;

namespace MoveBangladesh.CustomerAPI.Controllers
{
	public class CustomerAdminController : BaseAdminController<Customer>
	{
		public CustomerAdminController(IBaseRepository<Customer> repository) : base(repository)
		{

		}
	}
}
