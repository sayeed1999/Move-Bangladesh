using RideSharing.Application.Abstractions;
using RideSharing.Domain.Entities;

namespace RideSharing.CustomerAPI.Controllers
{
	public class DriverRatingAdminController : BaseAdminController<DriverRating>
	{
		public DriverRatingAdminController(IBaseRepository<DriverRating> repository) : base(repository)
		{

		}
	}
}
