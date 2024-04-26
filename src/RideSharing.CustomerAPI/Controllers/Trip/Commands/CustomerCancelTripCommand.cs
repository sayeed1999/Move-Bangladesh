using MediatR;
using Microsoft.AspNetCore.Mvc;
using RideSharing.Application.Trip.Commands.CustomerCancelTrip;
using RideSharing.Common.Entities;
using System.ComponentModel.DataAnnotations;

namespace RideSharing.CustomerAPI.Controllers.Trip.Commands
{
	[Route("api/external/trips")]
	[ApiController]
	public class CustomerCancelTripCommand(IMediator mediator) : ControllerBase
	{
		/// <summary>
		/// Use this endpoint to cancel active trip for customer
		/// </summary>
		/// <param name="tripId"></param>
		/// <param name="customerId"></param>
		/// <returns></returns>
		[HttpPut("{tripId}/cancel-by-customer")]
		public async Task<ActionResult<Response<Guid>>> Cancel([Required] Guid tripId, CustomerCancelTripCommandDto model)
		{
			model.CustomerId = new Guid(); // TODO: fetch from HttpContextAccessor
			model.TripId = tripId;

			var res = await mediator.Send(model);

			if (res.IsFailure) return BadRequest(res.Error);
			return Ok($"Ride {res.Value} has been canceled.");
		}
	}
}
