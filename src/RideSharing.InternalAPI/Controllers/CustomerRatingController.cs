using RideSharing.Application.Abstractions;
using RideSharing.Domain.Entities;

namespace RideSharing.InternalAPI.Controllers
{
	public class CustomerRatingController : BaseController<CustomerRating>
	{
		public CustomerRatingController(IBaseRepository<CustomerRating> repository) : base(repository)
		{

		}
	}
}
