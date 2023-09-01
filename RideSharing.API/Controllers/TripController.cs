using AuthService.Entity;
using CSharpFunctionalExtensions;
using Microsoft.AspNetCore.Mvc;
using RideSharing.Common.Entities;
using RideSharing.Entity;
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
        private readonly ITripService tripService;

        public TripController(ITripService tripService) : base(tripService)
        {
            this.tripService = tripService;
        }

        [HttpPost("request")]
        public async Task<ActionResult<Response<Trip>>> RequestRide(Trip model)
        {
            Result<Trip> trip = Trip.CreateNewTrip(model.CustomerId, model.DriverId, model.Source, model.Destination);
            if (trip.IsFailure) return BadRequest("Please provide valid data.");

            var res = await tripService.AddAsync(trip.Value);

            return Ok($"Ride request {res.Id} submitted successfully.");

        }


        [HttpPut("{tripId}/update")]
        public async Task<ActionResult<Response<Trip>>> Update(int tripId, TripStatus status)
        {
            var rideRequest = await tripService.FindByIdAsync(tripId);

            if (rideRequest == null)
                return NotFound($"Ride request {tripId} not found.");

            var trip = Trip.Modify(tripId, status);
            var res = await tripService.UpdateAsync(trip.Value);

            return Ok($"Ride request {res.Id} has been canceled.");
        }


        [HttpGet("{tripId}/status")]
        public async Task<ActionResult<Response<Trip>>> GetRideStatus(int tripId)
        {
            var rideRequest = await tripService.FindByIdAsync(tripId);

            if (rideRequest == null)
                return NotFound($"Ride request {tripId} not found.");

            return Ok($"Ride request {tripId} status: {rideRequest.Status}");
        }

    }
}