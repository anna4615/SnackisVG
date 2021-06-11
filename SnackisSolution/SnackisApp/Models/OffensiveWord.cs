using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SnackisApp.Models
{
    public class OffensiveWord
    {
        [JsonPropertyName("id")]
        public int Id { get; set; }

        [JsonPropertyName("forbiddenWord")]
        [Display(Name = "Ord")]
        [Required(ErrorMessage = "Fältet kan inte vara tomt")]
        public string Word { get; set; }
    }
}
