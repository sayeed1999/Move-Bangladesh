using BlogService.Entity;
using BlogService.Service.UserService;
using Microsoft.AspNetCore.Mvc;
using Sayeed.NTier.Generic.Controller;

namespace BlogService.API.Controllers
{
    [ApiController]
    [Route("api/v1/users")]
    public class UserController : BaseController<User>
    {
        public UserController(IUserService baseService) : base(baseService)
        {
        }
    }
}