using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogService.Entity
{
    public class Node : Audit
    {
        public NodeType NodeType { get; set; }
        public string Text { get; set; }
    }
}
