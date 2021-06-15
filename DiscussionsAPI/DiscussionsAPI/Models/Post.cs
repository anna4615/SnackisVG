using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PostsAPI.Models
{
    public class Post
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int SubjectId { get; set; }
        public string Title { get; set; }
        public int? PostId { get; set; }
        public DateTime Date { get; set; }
        public string Text { get; set; }
        public int NumberOfLike { get; set; }
        public int NumberOfLove { get; set; }
        public bool IsOffensiv { get; set; } = false;
        public int? GroupId { get; set; }
        public ICollection<PostImage> Images { get; set; }
        public ICollection<Post> Posts { get; set; }
    }
}
