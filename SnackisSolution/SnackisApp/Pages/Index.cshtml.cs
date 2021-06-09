using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SnackisApp.Areas.Identity.Data;
using SnackisApp.Data;
using SnackisApp.Gateways;
using SnackisApp.HelpMethods;
using SnackisApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnackisApp.Pages
{
    public class IndexModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public IndexModel(
            RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }


        [BindProperty]
        public SnackisUser FirstUser { get; set; }      


        public async Task<IActionResult> OnGetAsync()
        {
            if (_roleManager.Roles.Count() == 0)
            {
                var adminRole = new IdentityRole
                {
                    Name = "Admin"
                };
                await _roleManager.CreateAsync(adminRole);

                var userRole = new IdentityRole
                {
                    Name = "Medlem"
                };
                await _roleManager.CreateAsync(userRole);
            }           

            return Page();
        }
    }
}
