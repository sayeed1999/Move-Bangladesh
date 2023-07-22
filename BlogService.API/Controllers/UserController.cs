using BlogService.Entity;
using BlogService.Service.UserService;
using Microsoft.AspNetCore.Mvc;
using Sayeed.NTier.Generic.Controller;
using Sayeed.NTier.Generic.Logic;
using Sayeed.NTier.Generic.Repository;

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
