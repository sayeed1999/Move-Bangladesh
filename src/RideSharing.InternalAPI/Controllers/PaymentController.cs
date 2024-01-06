using RideSharing.Application.Abstractions;
using RideSharing.Domain.Entities;

namespace RideSharing.InternalAPI.Controllers
{
	public class PaymentController : BaseController<Payment>
	{
		public PaymentController(IPaymentRepository repository) : base(repository)
		{

		}
	}
}
