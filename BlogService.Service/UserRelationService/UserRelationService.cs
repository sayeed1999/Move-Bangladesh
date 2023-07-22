using BlogService.Entity;
using BlogService.Entity.Entities;
using BlogService.Service.UserRelationRepository;
using BlogService.Service.UserRelationService;
using BlogService.Service.UserRepository;
using RideSharing.Common.Entities;
using Sayeed.NTier.Generic.Logic;
using Sayeed.NTier.Generic.Repository;
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

        public UserRelationService(
            IUserRepository userRepository,
            IUserRelationRepository userRelationRepository
        ) : base(userRelationRepository)
        {
            this.userRepository = userRepository;
            this.userRelationRepository = userRelationRepository;
        }

        // TODO:- provide very clear messages with each error so that dev can track whats happening!
        public async Task<UserRelation> RequestFriendship(UserRelation userRelation)
        {
            userRelation.RelationType = RelationType.FriendRequestSent;

            // TODO:- 1. parse current user and set to from user id

            if (userRelation.FromUserId == userRelation.ToUserId)
                throw new IOException("Two users cannot be same!", 400);

            // check if to any user doesn't
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
        public async Task<UserRelation> ResponseFriendship(UserRelation userRelation)
        {
            // Note: here ToUser is who will respond!

            // TODO:- set current user
            long currentUserId = 0;

            // TODO:- dont allow to modify if from user is different from current user
            currentUserId = userRelation.ToUserId;

            // check if friend request exists
            var userRelationInDB = await this.userRelationRepository.FirstOrDefaultAsync(
                x => x.FromUserId == userRelation.FromUserId 
                && x.ToUserId == userRelation.ToUserId);

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

            if (userRelation.RelationType == RelationType.Unfriended)
            {
                // can unfriend only if user is a friend
                if (userRelationInDB.RelationType != RelationType.FriendRequestAccepted)
                    throw new CustomException("User is not a friend!", 400);

                // deleted relationship entity
                await this.DeleteByIdAsync(userRelationInDB.Id);
                return userRelationInDB;
            }


            // can block at any state

            userRelationInDB.RelationType = userRelation.RelationType;
            await this.UpdateAsync(userRelationInDB);
            return userRelation;
        }

    }
}
