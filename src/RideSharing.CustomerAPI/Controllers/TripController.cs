using Microsoft.AspNetCore.Mvc;
using RideSharing.Application.TripRequests.Commands.StartTrip;
using RideSharing.Application.Trips.Commands.EndTrip;

namespace RideSharing.CustomerAPI.Controllers
{
	[Route("api/trips")]
	public class TripController : BaseController
	{
		/// <summary>
		/// Use this endpoint to accept trip request for driver
		/// </summary>
		/// <param name="tripRequestId"></param>
		/// <returns></returns>
		[HttpPut("{tripRequestId}/start-trip")]
		public async Task<ActionResult> StartTrip(string tripRequestId)
		{
			var driverId = string.Empty; // TODO:- get customerId from httpContextAccessor!

			var model = new StartTripCommandDto(driverId, tripRequestId);

			var res = await Mediator.Send(model);

			if (res.IsFailure) return BadRequest(res.Error);

			return Created();
		}

		/// <summary>
		/// Call this endpoint from driver's end when destination has been reached.
		/// </summary>
		/// <param name="tripId"></param>
		/// <returns></returns>
		[HttpPut("{tripId}/end-trip")]
		public async Task<ActionResult> Handle(string tripId)
		{
			var driverId = string.Empty; // TODO:- get driverId from httpContextAccessor!

			var model = new EndTripDto(driverId, tripId);

			var res = await Mediator.Send(model);

			if (res.IsFailure) return BadRequest(res.Error);

			return Ok(res.Value);
		}
	}
}
