using BlogService.Entity;
using BlogService.Entity.Entities;
using BlogService.Service.EdgeRepository;
using BlogService.Service.NodeRepository;
using BlogService.Service.UserRelationRepository;
using BlogService.Service.UserRelationService;
using BlogService.Service.UserRepository;
using Microsoft.EntityFrameworkCore;
using RideSharing.Common.Entities;
using Sayeed.Generic.OnionArchitecture.Logic;
using Sayeed.Generic.OnionArchitecture.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogService.Service.UserService
{
    public class UserRelationService : BaseService<UserRelation>, IUserRelationService
    {
        private readonly IUserRepository userRepository;
        private readonly IUserRelationRepository userRelationRepository;
        private readonly INodeRepository nodeRepository;
        private readonly IEdgeRepository edgeRepository;

        public UserRelationService(
            IUserRepository userRepository,
            IUserRelationRepository userRelationRepository,
            INodeRepository nodeRepository,
            IEdgeRepository edgeRepository
        ) : base(userRelationRepository)
        {
            this.userRepository = userRepository;
            this.userRelationRepository = userRelationRepository;
            this.nodeRepository = nodeRepository;
            this.edgeRepository = edgeRepository;
        }

        // TODO:- provide very clear messages with each error so that dev can track whats happening!
        public async Task<UserRelation> SendFriendRequest(UserRelation userRelation)
        {
            userRelation.RelationType = RelationType.FriendRequestSent;

            // TODO:- 1. parse current user and set to from user id

            if (userRelation.FromUserId == userRelation.ToUserId)
                throw new IOException("Two users cannot be same!", 400);

            // check if any user doesn't exist
            var userInDB = await this.userRepository.FindByIdAsync(userRelation.FromUserId);
            if (userInDB is null)
                throw new IOException("First user doesn't exist!", 400);

            userInDB = await this.userRepository.FindByIdAsync(userRelation.ToUserId);
            if (userInDB is null)
                throw new IOException("Second user doesn't exist!", 400);

            // check if one has already a pending request on another or already friends
            var userRelationInDB = await this.userRelationRepository.FirstOrDefaultAsync(
                filter: x => (x.FromUserId == userRelation.FromUserId 
                                  && x.ToUserId == userRelation.ToUserId
                                  && (x.RelationType == RelationType.FriendRequestSent
                                  || x.RelationType == RelationType.FriendRequestAccepted))
                              || (x.FromUserId == userRelation.ToUserId
                                  && x.ToUserId == userRelation.FromUserId
                                  && (x.RelationType == RelationType.FriendRequestSent
                                  || x.RelationType == RelationType.FriendRequestAccepted)));

            if (userRelationInDB is not null)
                throw new CustomException("Cannot re-send friend request!", 400);

            await this.AddAsync(userRelation);
            return userRelation;
        }

        // TODO:- provide very clear messages with each error so that dev can track whats happening!
        public async Task<UserRelation> ResponseFriendRequest(UserRelation userRelation)
        {
            // Note: here ToUser is who will respond!

            // TODO:- set current user
            long currentUserId = 0;

            // TODO:- dont allow to modify if from user is different from current user
            currentUserId = userRelation.ToUserId;

            // unfriend user
            if (userRelation.RelationType >= RelationType.Unfriended)
                throw new CustomException("Cannot unfriend user using this endpoint!", 400);

            // check if friend request exists
            var userRelationInDB = await CheckIfUserRelationExists(userRelation);
            if (userRelationInDB is null)
                throw new CustomException("Friend request not found!", 404);

            // TODO:- get name of userRelation.RelationType and use in {} in place of this state.
            if (userRelation.RelationType == userRelationInDB.RelationType)
                throw new CustomException($"User Relationship is already in this state", 400);

            // can accept or reject request only if there is a pending request
            if ((userRelation.RelationType == RelationType.FriendRequestAccepted
                || userRelation.RelationType == RelationType.FriendRequestRejected)
                && userRelationInDB.RelationType != RelationType.FriendRequestSent)
                throw new CustomException("Friend request not found!", 400);

            // accept friend request
            if (userRelation.RelationType == RelationType.FriendRequestAccepted
                && userRelationInDB.RelationType == RelationType.FriendRequestSent)
            {
                await this.MakeFriendsAsync(userRelationInDB);
                return userRelationInDB;
            }

            /*{
                // can unfriend only if user is a friend
                
                await this.UnfriendUser(userRelationInDB);
                return userRelationInDB;
            }*/

            userRelationInDB.RelationType = userRelation.RelationType;
            await this.UpdateAsync(userRelationInDB);
            return userRelation;
        }

        private async Task<UserRelation> CheckIfUserRelationExists(UserRelation userRelation)
        {
            return await this.userRelationRepository.FirstOrDefaultAsync(x => x.FromUserId == userRelation.FromUserId
                                                                        && x.ToUserId == userRelation.ToUserId);
        }

        private async Task MakeFriendsAsync(UserRelation userRelation)
        {
            // here fromUser sent request, toUser accepted request, so toUser is the currentUser!

            // check if nodes exist or add them
            var userA = await this.nodeRepository.CreateNodeForUserIfNotExistsAsync(userRelation.FromUserId);
            var userB = await this.nodeRepository.CreateNodeForUserIfNotExistsAsync(userRelation.ToUserId);

            // establish bi-directional edges between nodes for A is a friend to B & B is a friend to A
            var edgeAToB = await this.edgeRepository.CreateEdgeIfNotExistsAsync(userA.Id, userB.Id, EdgeType.Friend, userRelation.ToUserId);
            var edgeBToA = await this.edgeRepository.CreateEdgeIfNotExistsAsync(userB.Id, userA.Id, EdgeType.Friend, userRelation.ToUserId);

            // update user relation entity
            userRelation.RelationType = RelationType.FriendRequestAccepted;
            await this.userRelationRepository.UpdateAsync(userRelation);
        }

        public async Task<UserRelation> UnfriendUserAsync(UserRelation userRelation)
        {
            // check if they are already friends
            var userRelationInDB = await this.userRelationRepository.FirstOrDefaultAsync(
                filter: x => (x.FromUserId == userRelation.FromUserId
                                  && x.ToUserId == userRelation.ToUserId
                                  && x.RelationType == RelationType.FriendRequestAccepted)
                              || (x.FromUserId == userRelation.ToUserId
                                  && x.ToUserId == userRelation.FromUserId
                                  && x.RelationType == RelationType.FriendRequestAccepted));


            if (userRelationInDB.RelationType != RelationType.FriendRequestAccepted)
                throw new CustomException("User is not a friend!", 400);

            // find user nodes
            var userA = await this.nodeRepository.FirstOrDefaultAsync(x => x.CreatedById == userRelation.FromUserId);
            var userB = await this.nodeRepository.FirstOrDefaultAsync(x => x.CreatedById == userRelation.ToUserId);

            var edgeAToB = await this.edgeRepository.DeleteEdgeIfExistsAsync(userA.Id, userB.Id, EdgeType.Friend);
            var edgeBToA = await this.edgeRepository.DeleteEdgeIfExistsAsync(userB.Id, userA.Id, EdgeType.Friend);

            // deleted user relation entity
            await this.DeleteByIdAsync(userRelationInDB.Id);
            return userRelationInDB;
        }

    }
}
