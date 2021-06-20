using SnackisApp.Areas.Identity.Data;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace SnackisApp.Models
{
    public class Group
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        [Display(Name = "Namn")]
        [Required(ErrorMessage = "Gruppen måste ha ett namn.")]
        public string Name { get; set; }

        public virtual ICollection<Membership> Memberships { get; set; }
    }
}
