using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RideSharing.Application;
using RideSharing.Domain;
using Sayeed.Generic.OnionArchitecture.Controller;

namespace RideSharing.API
{
    [AllowAnonymous]
    [Route("api/v1/customers")]
    [ApiController]
    public class CustomerController : BaseController<Customer>
    {
        public CustomerController(ICustomerService baseService) : base(baseService)
        {
        }
    }
}