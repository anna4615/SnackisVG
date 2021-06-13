using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SnackisApp.Models
{
    public class PostImage
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("postId")]
        public int PostId { get; set; }

        [JsonPropertyName("fileName")]
        public string FileName { get; set; }
    }
}
