using BlogService.Entity;
using BlogService.Entity.Dtos;
using BlogService.Entity.Entities;
using BlogService.Infrastructure;
using RideSharing.Common.Entities;
using Sayeed.Generic.OnionArchitecture.Repository;
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

        public async Task<Node> CreateNodeForUserIfNotExistsAsync(long userId)
        {
            var userNode = await this.FirstOrDefaultAsync(x => x.NodeType == NodeType.User
                                                        && x.CreatedById == userId);

            if (userNode is null)
            {
                userNode = new Node
                {
                    NodeType = NodeType.User,
                    CreatedById = userId,
                };
                await this.AddAsync(userNode);
            }
            return userNode;
        }

        public async Task<Node> CreateNodeForPostIfNotExistsAsync(PostDto post)
        {
            var postNode = new Node
            {
                Text = post.Text,
                CreatedById = post.UpdatedById,
            };
            await this.AddAsync(postNode);

            return postNode;
        }

    }
}
