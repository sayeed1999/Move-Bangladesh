using MediatR;
using Microsoft.AspNetCore.Mvc;
using RideSharing.Application.Trip.Commands.InitiatePayment;
using RideSharing.Common.Entities;
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
		public async Task<ActionResult<Response<long>>> Handle(
            long tripId, 
            PaymentMethod paymentMethod)
		{
			var customerId = new long(); // TODO:- get customerId from httpContextAccessor!

			var model = new InitiatePaymentDto(customerId, tripId, paymentMethod);

			var res = await mediator.Send(model);

			if (res.IsFailure) return BadRequest(res.Error);

			return Ok(res.Value);
		}
	}
}
