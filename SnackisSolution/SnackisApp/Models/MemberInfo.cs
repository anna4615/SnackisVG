using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SnackisApp.Models
{
    public class MemberInfo
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        [Display(Name = "Text till din infosida")]
        public string Text { get; set; }
    }
}