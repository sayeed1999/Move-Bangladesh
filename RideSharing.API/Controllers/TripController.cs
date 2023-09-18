using AuthService.Entity;
using CSharpFunctionalExtensions;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using RideSharing.Common.Entities;
using RideSharing.Entity;
using RideSharing.Entity.Dtos;
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
        private readonly IMediator _mediator;
        private readonly ITripService tripService;

        public TripController(
            IMediator mediator,
            ITripService tripService) : base(tripService)
        {
            this._mediator = mediator;
            this.tripService = tripService;
        }
    }
}