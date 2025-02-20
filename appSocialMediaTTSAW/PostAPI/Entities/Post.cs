using PostAPI.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostAPI.Entities
{
    public class Post
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        //public ApplicationUser User { get; set; } 

        public string Content { get; set; }
        public ICollection<Comment>? Comments { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

    }
}


