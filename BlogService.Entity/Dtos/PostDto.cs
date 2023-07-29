using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlogService.Entity.Dtos
{
    public class PostDto
    {
        public long Id { get; set; } = 0;
        public string Text { get; set; } = string.Empty;
        public long UpdatedById { get; set; }
        public DateTime UpdatedAtUtc { get; set; } = DateTime.UtcNow;
    }
}
