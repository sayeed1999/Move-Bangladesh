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
        Authored = 1,
        AuthoredBy = 2,

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