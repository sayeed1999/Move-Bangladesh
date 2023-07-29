using BlogService.Entity.Entities;
using BlogService.Service.UserRelationService;
using BlogService.Service.UserService;
using Microsoft.AspNetCore.Mvc;
using Sayeed.Generic.OnionArchitecture.Controller;
using Sayeed.Generic.OnionArchitecture.Logic;

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


        [HttpPost("send-friend-request")]
        public async Task<IActionResult> RequestFriendshipAsync([FromBody] UserRelation userRelation)
        {
            var result = await userRelationService.SendFriendRequest(userRelation);
            return Ok(result);
        }

        [HttpPost("response-friend-request")]
        public async Task<IActionResult> ResponseFriendshipAsync([FromBody] UserRelation userRelation)
        {
            var result = await userRelationService.ResponseFriendRequest(userRelation);
            return Ok(result);
        }

        [HttpPost("unfriend-user")]
        public async Task<IActionResult> UnfriendUserAsync([FromBody] UserRelation userRelation)
        {
            var result = await userRelationService.UnfriendUserAsync(userRelation);
            return Ok(result);
        }

    }
}
