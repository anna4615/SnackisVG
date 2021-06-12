using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SnackisApp.Models
{
    public class Subject
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("forumId")]
        public int ForumId { get; set; }

        [JsonPropertyName("name")]
        [Display(Name = "Namn")]
        public string Name { get; set; }

        [JsonPropertyName("posts")]
        public List<Post> Posts { get; set; }
    }
}
