using BlogService.Entity;
using BlogService.Entity.Dtos;
using Sayeed.Generic.OnionArchitecture.Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogService.Service.PostService
{
    public interface IPostService : IBaseService<Node>
    {
        Task<PostDto> CreatePost(PostDto post);
    }
}
