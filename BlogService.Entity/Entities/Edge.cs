namespace BlogService.Entity
{
    public class Edge : Audit
    {
        public EdgeType EdgeType { get; set; }
        public Node FromDestination { get; set; }
        public long FromDestinationId { get; set; }
        public Node ToDestination { get; set; }
        public long ToDestinationId { get; set; }
    }
}