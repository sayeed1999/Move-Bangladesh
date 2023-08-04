namespace BlogService.Entity
{
    public class Node : Audit
    {
        public NodeType NodeType { get; set; }
        public string Text { get; set; } = string.Empty;
    }
}