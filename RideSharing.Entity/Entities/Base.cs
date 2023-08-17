namespace RideSharing.Entity
{
    public abstract class Base
    {

        public long Id { get; set; }
        public DateTime CreatedDateUtc { get; set; } = DateTime.UtcNow;
        public DateTime? UpdatedDateUtc { get; set; }
        public DateTime? DeletedDateUtc { get; set; }
    }
}