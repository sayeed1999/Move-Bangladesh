using RideSharing.Application.Abstractions;
using RideSharing.Domain.Entities;

namespace RideSharing.CustomerAPI.Controllers
{
	public class CabAdminController : BaseAdminController<Cab>
	{
		public CabAdminController(IBaseRepository<Cab> repository) : base(repository)
		{

		}
	}
}
