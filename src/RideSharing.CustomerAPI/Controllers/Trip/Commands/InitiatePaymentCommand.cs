using MediatR;
using Microsoft.AspNetCore.Mvc;
using RideSharing.Application.Trips.Commands.InitiatePayment;
using RideSharing.Domain.Entities;

namespace RideSharing.CustomerAPI.Controllers.Trip.Commands
{
	[Route("api/external/trips")]
	[ApiController]
	public class InitiatePaymentCommand(
		IMediator mediator) : ControllerBase
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

			var model = new InitiatePaymentDto(customerId, tripId, paymentMethod);

			var res = await mediator.Send(model);

			if (res.IsFailure) return BadRequest(res.Error);

			return Ok(res.Value);
		}
	}
}
