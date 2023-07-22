using BlogService.Entity.Entities;
using BlogService.Service.UserRelationService;
using BlogService.Service.UserService;
using Microsoft.AspNetCore.Mvc;
using Sayeed.NTier.Generic.Controller;
using Sayeed.NTier.Generic.Logic;

namespace BlogService.API.Controllers
{
    [ApiController]
    [Route("api/v1/user-relations")]
    public class UserRelationController : ControllerBase
    {
        private readonly IUserRelationService userRelationService;

        public UserRelationController(IUserRelationService userRelationService)
        {
            this.userRelationService = userRelationService;
        }


        [HttpPost("request-friendship")]
        public async Task<IActionResult> RequestFriendshipAsync([FromBody] UserRelation userRelation)
        {
            var result = await userRelationService.RequestFriendship(userRelation);
            return Ok(result);
        }

        [HttpPut("response-friendship")]
        public async Task<IActionResult> ResponseFriendshipAsync([FromBody] UserRelation userRelation)
        {
            var result = await userRelationService.ResponseFriendship(userRelation);
            return Ok(result);
        }


    }
}
