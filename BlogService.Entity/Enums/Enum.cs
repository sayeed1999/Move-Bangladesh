using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogService.Entity
{
    public enum NodeType
    {
        Unidentified = 0,
        User = 1,
        Post = 2,
        Comment = 3,
        Reply = 4,
    }

    public enum EdgeType
    {
        Unidentified = 0,
        Posted = 1,
        PostedBy = 2,
        Commented = 3,
        CommentedBy = 4,
        Replied = 5,
        RepliedBy = 6,
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
}
