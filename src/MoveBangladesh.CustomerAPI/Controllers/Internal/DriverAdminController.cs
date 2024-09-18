using MoveBangladesh.Application.Abstractions;
using MoveBangladesh.Domain.Entities;

namespace MoveBangladesh.CustomerAPI.Controllers
{
	public class DriverAdminController : BaseAdminController<Driver>
	{
		public DriverAdminController(IBaseRepository<Driver> repository) : base(repository)
		{

		}
	}
}
