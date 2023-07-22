using BlogService.Entity;
using Sayeed.NTier.Generic.Repository;
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
    }
}
