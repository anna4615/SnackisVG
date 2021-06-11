using SnackisApp.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SnackisApp.Models
{
    public class Post
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("userId")]
        public string UserId { get; set; }

        [JsonPropertyName("subjectId")]
        public int SubjectId { get; set; }
        
        [JsonPropertyName("subject")]
        public Subject Subject { get; set; }
        
        [JsonPropertyName("postId")]
        public int? PostId { get; set; }
        
        [JsonPropertyName("date")]
        public DateTime Date { get; set; }

        [JsonPropertyName("title")]
        [Display(Name = "Rubrik")]
        public string Title { get; set; }

        [JsonPropertyName("text")]
        [Display(Name = "Skriv ditt inlägg här")]
        [Required(ErrorMessage = "Fältet kan inte vara tomt")]
        public string Text { get; set; }
        
        [JsonPropertyName("isOffensiv")]
        public bool IsOffensiv { get; set; }
    }
}
