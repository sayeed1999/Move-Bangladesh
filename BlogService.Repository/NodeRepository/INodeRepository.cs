using BlogService.Entity;
using BlogService.Entity.Dtos;
using Sayeed.Generic.OnionArchitecture.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogService.Service.NodeRepository
{
    public interface INodeRepository : IBaseRepository<Node>
    {
        Task<Node> CreateNodeForUserIfNotExistsAsync(long userId);
        Task<Node> CreateNodeForPostIfNotExistsAsync(PostDto post);

    }
}
