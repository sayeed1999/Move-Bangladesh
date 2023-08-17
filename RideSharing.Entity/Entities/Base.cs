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

        public long Id { get; private set; }
        public DateTime CreatedDateUtc { get; private set; }
        public DateTime? UpdatedDateUtc { get; private set; }
        public DateTime? DeletedDateUtc { get; private set; }
    }
}