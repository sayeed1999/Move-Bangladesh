using BlogService.Entity;
using BlogService.Entity.Dtos;
using BlogService.Service.PostService;
using Microsoft.AspNetCore.Mvc;

namespace BlogService.API.Controllers
{
    [ApiController]
    [Route("api/v1/newsfeed/posts")]
    public class PostController : ControllerBase
    {
        private readonly IPostService postService;

        public PostController(IPostService postService)
        {
            this.postService = postService;
        }

        [HttpPost]
        public virtual async Task<ActionResult> Add([FromBody] PostDto post)
        {
            var res = await postService.CreatePost(post);
            return Ok(post);
        }


    }
}
