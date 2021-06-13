using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostsAPI.Models
{
    public class PostImage
    {
        public int Id { get; set; }
        public int PostId { get; set; }
        public string FileName { get; set; }
    }
}
