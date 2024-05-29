using RideSharing.Application.Abstractions;
using RideSharing.Domain.Entities;

namespace RideSharing.InternalAPI.Controllers
{
	public class PaymentController : BaseController<PaymentEntity>
	{
		public PaymentController(IBaseRepository<PaymentEntity> repository) : base(repository)
		{

		}
	}
}
