using BlogService.Entity;
using BlogService.Entity.Entities;
using BlogService.Service.UserService;
using Microsoft.AspNetCore.Mvc;
using Sayeed.Generic.OnionArchitecture.Controller;

namespace BlogService.API.Controllers
{
    [ApiController]
    [Route("api/v1/users")]
    public class UserController : BaseController<User>
    {
        private readonly IUserService userService;

        public UserController(IUserService userService) : base(userService)
        {
            this.userService = userService;
        }

    }
}
