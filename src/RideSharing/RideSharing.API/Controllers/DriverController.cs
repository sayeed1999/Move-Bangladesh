using Microsoft.AspNetCore.Mvc;
using RideSharing.Domain;
using RideSharing.Service;
using Sayeed.Generic.OnionArchitecture.Controller;

namespace RideSharing.API
{
    [Route("api/v1/drivers")]
    [ApiController]
    public class DriverController : BaseController<Driver>
    {
        public DriverController(IDriverService baseService) : base(baseService)
        {
        }
    }
}