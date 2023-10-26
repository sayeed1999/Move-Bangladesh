namespace RideSharing.Entity
{
    public abstract class Base
    {
        protected Base()
        {
            Id = 0;
            CreatedDateUtc = DateTime.UtcNow;
            UpdatedDateUtc = DateTime.UtcNow;
            DeletedDateUtc = null;
        }

        public long Id { get; protected set; }
        public DateTime CreatedDateUtc { get; protected set; }
        public DateTime? UpdatedDateUtc { get; protected set; }
        public DateTime? DeletedDateUtc { get; protected set; }
    }
}