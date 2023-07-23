using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogService.Entity
{
    public enum NodeType
    {
        User = 1,
        
        Post = 5,
        Comment = 6,
        Reply = 7,

        FacebookTimeline = 10,
        FacebookGroup = 11,
        FacebookPage = 12,
    }

    public enum EdgeType
    {
        Friend = 1,

        Authored = 5,
        AuthoredBy = 6,

        Liked = 7,
        LikedBy = 8,
        Shared = 9,
        SharedBy = 10,
        Tagged = 11,
        TaggedAt = 12,
        
        ReactedLove = 50,
        ReactedLoveBy = 51,
        ReactedHaha = 52,
        ReactedHahaBy = 53,
        ReactedAngry = 54,
        ReactedAngryBy = 55,
        ReactedCrying = 56,
        ReactedCryingBy = 57,
        
        Reported = 100,
        ReportedBy = 101,
    }

    public enum RelationType
    {
        FriendRequestSent = 1,
        FriendRequestCanceled = 2,
        FriendRequestAccepted = 3,
        FriendRequestRejected = 4,
        Unfriended = 5,
        Blocked = 6,
    }
}
