using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace SnackisApp.Areas.Identity.Data
{
    // Add profile data for application users by adding properties to the SnackisUser class
    public class SnackisUser : IdentityUser
    {
        [PersonalData]
        [Required]
        [Display(Name = "Förnamn")]
        public string FirstName { get; set; }

        [PersonalData]
        [Required]
        [Display(Name = "Efternamn")]
        public string LastName { get; set; }

        [PersonalData]
        public string Picture { get; set; }     

    }
}
