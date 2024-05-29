using RideSharing.Application.Abstractions;
using RideSharing.Domain.Entities;

namespace RideSharing.InternalAPI.Controllers
{
	public class TripController : BaseController<TripEntity>
	{
		public TripController(IBaseRepository<TripEntity> repository) : base(repository)
		{

		}
	}
}
