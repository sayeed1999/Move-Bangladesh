namespace BlogService.Entity
{
    public abstract class Audit
    {
        public long Id { get; set; }
        public User CreatedBy { get; set; }
        public long CreatedById { get; set; }
        public DateTime CreatedDateUtc { get; set; } = DateTime.UtcNow;

        public User? UpdatedBy { get; set; }
        public long? UpdatedById { get; set; }
        public DateTime? UpdatedDateUtc { get; set; }

        public User? DeletedBy { get; set; }
        public long? DeletedById { get; set; }
        public DateTime? DeletedDateUtc { get; set; }
    }
}