using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostAPI.DTOs
{
    public class Post
    {
        public int UserId { get; set; }
        public string Content { get; set; }
        public string Fullname { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
