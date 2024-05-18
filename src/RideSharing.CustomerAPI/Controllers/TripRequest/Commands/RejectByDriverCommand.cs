using MediatR;
using Microsoft.AspNetCore.Mvc;
using RideSharing.Application.TripRequest.Commands.RejectByDriver;
using RideSharing.Common.Entities;
using System.ComponentModel.DataAnnotations;

namespace RideSharing.DriverAPI.Controllers.TripRequest.Commands
{
	[Route("api/external/trip-requests")]
	[ApiController]
	public class RejectByDriverCommand(IMediator mediator) : ControllerBase
	{
		/// <summary>
		/// Use this endpoint to cancel active trip for driver
		/// </summary>
		/// <param name="tripId"></param>
		/// <param name="driverId"></param>
		/// <returns></returns>
		[HttpPut("{tripRequestId}/reject-by-driver")]
		public async Task<ActionResult<Response<long>>> Cancel([Required] long tripRequestId, RejectByDriverCommandDto model)
		{
			model.DriverId = new long(); // TODO: fetch from HttpContextAccessor
			model.TripRequestId = tripRequestId;

			var res = await mediator.Send(model);

			if (res.IsFailure) return BadRequest(res.Error);
			return Ok($"Ride {res.Value} has been canceled.");
		}
	}
}
