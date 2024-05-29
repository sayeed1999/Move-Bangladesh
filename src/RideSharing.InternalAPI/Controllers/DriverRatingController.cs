using RideSharing.Application.Abstractions;
using RideSharing.Domain.Entities;

namespace RideSharing.InternalAPI.Controllers
{
	public class DriverRatingController : BaseController<DriverRatingEntity>
	{
		public DriverRatingController(IBaseRepository<DriverRatingEntity> repository) : base(repository)
		{

		}
	}
}
