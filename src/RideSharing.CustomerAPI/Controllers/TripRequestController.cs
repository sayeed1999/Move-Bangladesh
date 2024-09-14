using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;
using RideSharing.Application.TripRequests.Commands.AcceptTripRequest;
using RideSharing.Application.TripRequests.Commands.CancelTripRequest;
using RideSharing.Application.TripRequests.Commands.RejectByCustomer;
using RideSharing.Application.TripRequests.Commands.RejectByDriver;
using RideSharing.Application.TripRequests.Commands.TripRequests;

namespace RideSharing.CustomerAPI.Controllers
{
	[Route("api/v1/trip-requests")]
	public class TripRequestController : BaseController
	{
		/// <summary>
		/// Customer will request a trip using this endpoint by selecting source, destination, cabType.
		/// Initially the driverId will be null because driver is not chosen yet.
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPost]
		public async Task<ActionResult> RequestTrip(RequestTripCommand model)
		{
			var res = await Mediator.Send(model);

			if (res.IsFailure) return BadRequest(res.Error);

			return Created();
		}

		/// <summary>
		/// Use this endpoint to accept trip request for driver
		/// </summary>
		/// <param name="tripRequestId"></param>
		/// <returns></returns>
		[HttpPut("{tripRequestId}/accept")]
		public async Task<ActionResult> AcceptTripRequest(string tripRequestId)
		{
			var driverId = string.Empty; // TODO:- get customerId from httpContextAccessor!

			var model = new AcceptTripRequestDto(driverId, tripRequestId);

			var res = await Mediator.Send(model);

			if (res.IsFailure) return BadRequest(res.Error);

			return Ok(res.Value);
		}

		/// <summary>
		/// Customer will cancel a trip request using this endpoint
		/// Customer can cancel a trip request using this endpoint before it is accepted by a driver
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpPut("{tripRequestId}/cancel-by-customer")]
		public async Task<ActionResult> CancelTripRequest(string tripRequestId)
		{
			var customerId = string.Empty; // TODO:- get customerId from httpContextAccessor!

			var model = new CancelRequestTripCommandDto(customerId, tripRequestId);

			var res = await Mediator.Send(model);

			if (res.IsFailure) return BadRequest(res.Error);

			return Ok(res.Value);
		}

		/// <summary>
		/// Use this endpoint to cancel a accepted trip request for customer
		/// </summary>
		/// <param name="tripId"></param>
		/// <param name="customerId"></param>
		/// <returns></returns>
		[HttpPut("{tripRequestId}/reject-by-customer")]
		public async Task<ActionResult> RejectByCustomer([Required] string tripRequestId, RejectByCustomerCommandDto model)
		{
			model.CustomerId = string.Empty; // TODO: fetch from HttpContextAccessor
			model.TripRequestId = tripRequestId;

			var res = await Mediator.Send(model);

			if (res.IsFailure) return BadRequest(res.Error);
			return Ok($"Ride {res.Value} has been canceled.");
		}

		/// <summary>
		/// Use this endpoint to cancel active trip for driver
		/// </summary>
		/// <param name="tripId"></param>
		/// <param name="driverId"></param>
		/// <returns></returns>
		[HttpPut("{tripRequestId}/reject-by-driver")]
		public async Task<ActionResult> RejectByDriver([Required] string tripRequestId, RejectByDriverCommandDto model)
		{
			model.DriverId = string.Empty; // TODO: fetch from HttpContextAccessor
			model.TripRequestId = tripRequestId;

			var res = await Mediator.Send(model);

			if (res.IsFailure) return BadRequest(res.Error);
			return Ok($"Ride {res.Value} has been canceled.");
		}
	}
}
