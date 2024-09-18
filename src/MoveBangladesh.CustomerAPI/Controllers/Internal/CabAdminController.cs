using MoveBangladesh.Application.Abstractions;
using MoveBangladesh.Domain.Entities;

namespace MoveBangladesh.CustomerAPI.Controllers
{
	public class CabAdminController : BaseAdminController<Cab>
	{
		public CabAdminController(IBaseRepository<Cab> repository) : base(repository)
		{

		}
	}
}
