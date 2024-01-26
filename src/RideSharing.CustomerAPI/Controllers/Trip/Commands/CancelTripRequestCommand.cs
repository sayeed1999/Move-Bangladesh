using MediatR;
using Microsoft.AspNetCore.Mvc;
using RideSharing.Application.TripUseCase.Commands.CancelTripRequestCommand;
using RideSharing.Common.Entities;

namespace RideSharing.CustomerAPI.Controllers.Trip.Commands
{

	[Route("api/external/trip-requests")]
	[ApiController]
	public class CancelTripRequestCommand : ControllerBase
	{
		private readonly IMediator _mediator;

		public CancelTripRequestCommand(IMediator mediator)
		{
			this._mediator = mediator;
		}

		/// <summary>
		/// Customer will cancel a trip using this endpoint
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		[HttpDelete("{tripRequestId}")]
		public async Task<ActionResult<Response<CancelTripRequestCommandResponseDto>>> RequestRide(Guid tripRequestId)
		{
			var customerId = new Guid(); // TODO:- get customerId from httpContextAccessor!

			var model = new CancelTripRequestCommandDto(customerId, tripRequestId);

			var res = await _mediator.Send(model);

			if (res.IsFailure) return BadRequest(res.Error);

			return Ok(res.Value);
		}

	}
}
