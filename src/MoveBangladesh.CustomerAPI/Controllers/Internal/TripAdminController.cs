using MoveBangladesh.Application.Abstractions;
using MoveBangladesh.Domain.Entities;

namespace MoveBangladesh.CustomerAPI.Controllers
{
	public class TripAdminController : BaseAdminController<Trip>
	{
		public TripAdminController(IBaseRepository<Trip> repository) : base(repository)
		{

		}
	}
}
