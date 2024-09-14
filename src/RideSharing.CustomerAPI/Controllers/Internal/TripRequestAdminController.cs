using RideSharing.Application.Abstractions;
using RideSharing.Domain.Entities;

namespace RideSharing.CustomerAPI.Controllers
{
	public class TripRequestAdminController : BaseAdminController<Trip>
	{
		public TripRequestAdminController(IBaseRepository<Trip> repository) : base(repository)
		{

		}
	}
}
