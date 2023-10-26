﻿using MediatR;
using Microsoft.AspNetCore.Mvc;
using RideSharing.Common.Entities;
using RideSharing.Entity;
using RideSharing.Entity.Dtos;

namespace RideSharing.CustomerAPI.Controllers.TripCommands
{
    [Route("api/external/trips")]
    [ApiController]
    public class TripUpdateCommand : ControllerBase
    {
        private readonly IMediator _mediator;
    
        public TripUpdateCommand(IMediator mediator)
        {
            this._mediator = mediator;
        }

        [HttpPut("{tripId}/update")]
        public async Task<ActionResult<Response<Trip>>> Update(int tripId, TripModifyDto model)
        {
            model.TripId = tripId;
            var res = await _mediator.Send(model);
            if (res.IsFailure) return BadRequest(res.Error);
            return Ok($"Ride request {res.Value.Id} has been canceled.");
        }
    }
}