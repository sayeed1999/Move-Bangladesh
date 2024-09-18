using MoveBangladesh.Application.Abstractions;
using MoveBangladesh.Domain.Entities;

namespace MoveBangladesh.CustomerAPI.Controllers
{
	public class PaymentAdminController : BaseAdminController<Payment>
	{
		public PaymentAdminController(IBaseRepository<Payment> repository) : base(repository)
		{

		}
	}
}
