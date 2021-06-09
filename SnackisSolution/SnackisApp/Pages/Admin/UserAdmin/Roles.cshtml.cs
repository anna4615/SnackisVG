using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SnackisApp.Areas.Identity.Data;

namespace SnackisApp.Pages.Admin.UserAdmin
{
    public class RolesModel : PageModel
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RolesModel(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }


        [BindProperty]
        public string RoleName { get; set; }

        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            await CreateRole(RoleName);

            return RedirectToPage("./Roles");
        }

        private async Task CreateRole(string roleName)
        {
            if (await _roleManager.RoleExistsAsync(roleName) == false)
            {
                var role = new IdentityRole
                {
                    Name = roleName
                };

                await _roleManager.CreateAsync(role);
            }
        }
    }
}
