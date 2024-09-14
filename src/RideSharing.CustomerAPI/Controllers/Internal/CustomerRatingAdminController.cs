using RideSharing.Application.Abstractions;
using RideSharing.Domain.Entities;

namespace RideSharing.CustomerAPI.Controllers
{
	public class CustomerRatingAdminController : BaseAdminController<CustomerRating>
	{
		public CustomerRatingAdminController(IBaseRepository<CustomerRating> repository) : base(repository)
		{

		}
	}
}
