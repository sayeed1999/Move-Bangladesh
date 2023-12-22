using Microsoft.AspNetCore.Mvc;
using RideSharing.Domain;
using Sayeed.Generic.OnionArchitecture.Controller;
using RideSharing.Application.Abstractions;

namespace RideSharing.API
{
    [Route("api/v1/drivers")]
    [ApiController]
    public class DriverController : BaseController<Driver>
    {
        public DriverController(IDriverRepository baseService) : base(baseService)
        {
        }
    }
}