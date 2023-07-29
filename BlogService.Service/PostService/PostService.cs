using BlogService.Entity;
using BlogService.Entity.Dtos;
using BlogService.Service.EdgeRepository;
using BlogService.Service.NodeRepository;
using BlogService.Service.UserRepository;
using RideSharing.Common.Entities;
using Sayeed.Generic.OnionArchitecture.Logic;
using Sayeed.Generic.OnionArchitecture.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogService.Service.PostService
{
    public class PostService : BaseService<Node>, IPostService
    {
        private readonly IUserRepository userRepository;
        private readonly INodeRepository nodeRepository;
        private readonly IEdgeRepository edgeRepository;

        public PostService(
            IUserRepository userRepository,
            INodeRepository nodeRepository,
            IEdgeRepository edgeRepository
        ) : base(nodeRepository)
        {
            this.userRepository = userRepository;
            this.nodeRepository = nodeRepository;
            this.edgeRepository = edgeRepository;
        }

        public async Task<PostDto> CreatePost(PostDto post)
        {
            // TODO:- Step 0: set current user id
            var currentUserId = post.UpdatedById;

            // Step 1: check user exists
            var user = await this.userRepository.FindByIdAsync(post.UpdatedById);
            if (user is null)
            {
                throw new CustomException("User not found", 400);
            }

            // Step 2: create node
            var userNode = await this.nodeRepository.CreateNodeForUserIfNotExistsAsync(post.UpdatedById);
            var postNode = await this.nodeRepository.CreateNodeForPostIfNotExistsAsync(post);

            // Step 3: create edges
            var edgeA = await this.edgeRepository.CreateEdgeIfNotExistsAsync(
                userNode.Id, 
                postNode.Id, 
                EdgeType.Authored,
                currentUserId);

            var edgeB = await this.edgeRepository.CreateEdgeIfNotExistsAsync(
                postNode.Id,
                userNode.Id,
                EdgeType.AuthoredBy,
                currentUserId);

            return post;
        }
    }
}
