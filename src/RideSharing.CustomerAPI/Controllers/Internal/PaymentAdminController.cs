using RideSharing.Application.Abstractions;
using RideSharing.Domain.Entities;

namespace RideSharing.CustomerAPI.Controllers
{
	public class PaymentAdminController : BaseAdminController<Payment>
	{
		public PaymentAdminController(IBaseRepository<Payment> repository) : base(repository)
		{

		}
	}
}
