using MoveBangladesh.Application.Abstractions;
using MoveBangladesh.Domain.Entities;

namespace MoveBangladesh.CustomerAPI.Controllers
{
	public class DriverRatingAdminController : BaseAdminController<DriverRating>
	{
		public DriverRatingAdminController(IBaseRepository<DriverRating> repository) : base(repository)
		{

		}
	}
}
