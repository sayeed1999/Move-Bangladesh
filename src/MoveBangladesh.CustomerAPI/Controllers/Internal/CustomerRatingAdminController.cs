using MoveBangladesh.Application.Abstractions;
using MoveBangladesh.Domain.Entities;

namespace MoveBangladesh.CustomerAPI.Controllers
{
	public class CustomerRatingAdminController : BaseAdminController<CustomerRating>
	{
		public CustomerRatingAdminController(IBaseRepository<CustomerRating> repository) : base(repository)
		{

		}
	}
}
