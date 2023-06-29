using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RideSharing.Entity;
using RideSharing.Service;

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
