using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace SnackisApp.Models
{
    public class PrivateMessage
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int? PrivateMessageId { get; set; }

        public DateTime Date { get; set; }

        [Display(Name = "Rubrik")]
        public string Title { get; set; }

        [Display(Name = "Skriv ditt meddelande här")]
        public string Text { get; set; }

        [Display(Name = "Välj mottagare")]
        [Required(ErrorMessage = "Välj en mottagare")]
        public string ToUserName { get; set; }
    }
}
