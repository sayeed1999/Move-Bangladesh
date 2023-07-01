using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogService.Entity
{
    public class Node
    {
        public long Id { get; set; }
        public NodeType NodeType { get; set; }
        public User User { get; set; }
        public long UserId { get; set; }
        public string Text { get; set; }
    }
}
