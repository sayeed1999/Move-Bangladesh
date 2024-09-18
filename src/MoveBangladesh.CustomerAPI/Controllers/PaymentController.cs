using Microsoft.AspNetCore.Mvc;
using MoveBangladesh.Application.Trips.Commands.InitiatePayment;
using MoveBangladesh.Domain.Entities;

namespace MoveBangladesh.CustomerAPI.Controllers
{
	[Route("api/v1/payments")]
	public class PaymentController : BaseController
	{
		/// <summary>
		/// Call this endpoint from customer's end to initiate a payment.
		/// </summary>
		/// <param name="tripId"></param>
		/// <param name="paymentMethod"></param>
		/// <returns></returns>
		[HttpPut("{tripId}/initiate-payment/{paymentMethod}")]
		public async Task<ActionResult> Handle(
			string tripId,
			PaymentMethod paymentMethod)
		{
			var customerId = string.Empty; // TODO:- get customerId from httpContextAccessor!

			var model = new InitiatePaymentCommand(customerId, tripId, paymentMethod);

			var res = await Mediator.Send(model);

			if (res.IsFailure) return BadRequest(res.Error);

			return Ok(res.Value);
		}
	}
}
