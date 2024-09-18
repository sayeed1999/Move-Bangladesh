using MoveBangladesh.Application.Abstractions;
using MoveBangladesh.Domain.Entities;

namespace MoveBangladesh.CustomerAPI.Controllers
{
	public class TripRequestAdminController : BaseAdminController<Trip>
	{
		public TripRequestAdminController(IBaseRepository<Trip> repository) : base(repository)
		{

		}
	}
}
