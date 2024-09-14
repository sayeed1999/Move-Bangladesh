using RideSharing.Application.Abstractions;
using RideSharing.Domain.Entities;

namespace RideSharing.CustomerAPI.Controllers
{
	public class TripAdminController : BaseAdminController<Trip>
	{
		public TripAdminController(IBaseRepository<Trip> repository) : base(repository)
		{

		}
	}
}
