using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace OffensiveWordsAPI.Models
{
    public class OffensiveWord
    {
        public int Id { get; set; }

        [Required]
        public string ForbiddenWord { get; set; }

    }
}
