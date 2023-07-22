using BlogService.Entity;
using BlogService.Entity.Entities;
using BlogService.Infrastructure;
using Sayeed.NTier.Generic.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogService.Service.NodeRepository
{
    public class NodeRepository : BaseRepository<Node>, INodeRepository
    {
        public NodeRepository(AppDbContext dbContext) : base(dbContext)
        {

        }
    }
}
