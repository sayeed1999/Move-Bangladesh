using AuthService.Entity;
using Microsoft.AspNetCore.Mvc;
using RideSharing.Common.Entities;
using RideSharing.Entity;
using RideSharing.Entity.Entities;
using RideSharing.Entity.Enums;
using RideSharing.Infrastructure;
using RideSharing.Service;
using Sayeed.Generic.OnionArchitecture.Controller;

namespace RideSharing.API
{
    [Route("api/v1/trips")]
    [ApiController]
    public class TripController : BaseController<Trip>
    {

        private readonly ApplicationDbContext _dbContext;

        public TripController(ITripService baseService) : base(baseService)
        {
        }

        [HttpPost("request")]
        public async Task<ActionResult<Response<RideRequest>>> RequestRide(RideRequest model)
        {
            if (!ModelState.IsValid) // this one line with check "are all required fields of registerDto provided or not"
                throw new CustomException("Model is not valid!", 400);
           
            model.Status = RideStatusEnum.Pending;
            model.RequestTime = DateTime.UtcNow;

            _dbContext.RideRequests.Add(model);
            await _dbContext.SaveChangesAsync();

            return Ok($"Ride request {model.Id} submitted successfully.");

        }


        [HttpPut("cancel/{requestId}")]
        public async Task<IActionResult> CancelRide(int requestId)
        {
            var rideRequest = await _dbContext.RideRequests.FindAsync(requestId);

            if (rideRequest == null)
                return NotFound($"Ride request {requestId} not found.");

            if (rideRequest.Status == RideStatusEnum.Canceled)
                return BadRequest($"Ride request {requestId} is already canceled.");

            rideRequest.Status = RideStatusEnum.Canceled;
            await _dbContext.SaveChangesAsync();

            return Ok($"Ride request {requestId} has been canceled.");
        }


        [HttpGet("status/{requestId}")]
        public async Task<IActionResult> GetRideStatus(int requestId)
        {
            var rideRequest = await _dbContext.RideRequests.FindAsync(requestId);

            if (rideRequest == null)
                return NotFound($"Ride request {requestId} not found.");

            return Ok($"Ride request {requestId} status: {rideRequest.Status}");
        }
    }
}