using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PostAPI.DTOs
{
 
        public class Comment
        {
            public int UserId { get; set; }
            public int PostId { get; set; }
            public string Content { get; set; }
            public DateTime CreatedAt { get; set; } = DateTime.Now;


    }

}
