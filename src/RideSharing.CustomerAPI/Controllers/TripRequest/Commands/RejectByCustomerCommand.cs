using MediatR;
using Microsoft.AspNetCore.Mvc;
using RideSharing.Application.TripRequest.Commands.RejectByCustomer;
using System.ComponentModel.DataAnnotations;

namespace RideSharing.CustomerAPI.Controllers.TripRequest.Commands
{
	[Route("api/external/trip-requests")]
	[ApiController]
	public class RejectByCustomerCommand(IMediator mediator) : ControllerBase
	{
		/// <summary>
		/// Use this endpoint to cancel active trip for customer
		/// </summary>
		/// <param name="tripId"></param>
		/// <param name="customerId"></param>
		/// <returns></returns>
		[HttpPut("{tripRequestId}/reject-by-customer")]
		public async Task<ActionResult> Cancel([Required] string tripRequestId, RejectByCustomerCommandDto model)
		{
			model.CustomerId = string.Empty; // TODO: fetch from HttpContextAccessor
			model.TripRequestId = tripRequestId;

			var res = await mediator.Send(model);

			if (res.IsFailure) return BadRequest(res.Error);
			return Ok($"Ride {res.Value} has been canceled.");
		}
	}
}
