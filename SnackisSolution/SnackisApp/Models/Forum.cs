using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SnackisApp.Models
{
    public class Forum
    {        
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("name")]
        [Display(Name = "Forumnamn")]
        public string Name { get; set; }
        
        [JsonPropertyName("subjects")]
        public ICollection<Subject> Subjects { get; set; }
    }
}
