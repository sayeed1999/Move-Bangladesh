using RideSharing.Application.Abstractions;
using RideSharing.Domain.Entities;

namespace RideSharing.InternalAPI.Controllers
{
	public class DriverRatingController : BaseController<DriverRating>
	{
		public DriverRatingController(IDriverRatingRepository repository) : base(repository)
		{

		}
	}
}
