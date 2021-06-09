using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostsAPI.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public int ForumId { get; set; }
        public string Name { get; set; }
        public Forum Forum { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
