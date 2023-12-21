using Microsoft.AspNetCore.Mvc;
using RideSharing.Application;
using RideSharing.Domain;
using Sayeed.Generic.OnionArchitecture.Controller;

namespace RideSharing.API
{
    [Route("api/v1/cabs")]
    [ApiController]
    public class CabController : BaseController<Cab>
    {
        public CabController(ICabService baseService) : base(baseService)
        {
        }
    }
}