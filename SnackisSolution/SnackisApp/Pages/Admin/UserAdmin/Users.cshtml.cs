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
    public class UsersModel : PageModel
    {
        //private readonly RoleManager<IdentityRole> _roleManager;
        public readonly UserManager<SnackisUser> _userManager;

        public UsersModel(/*RoleManager<IdentityRole> roleManager,*/ UserManager<SnackisUser> userManager)
        {
            //_roleManager = roleManager;
            _userManager = userManager;
        }

        public bool UserIsAdmin { get; set; }
        //public bool IsMember { get; set; }

        //public SnackisUser CurrentUser { get; set; }
        //public List<SnackisUser> Users { get; set; }
        //public List<SnackisUser> Admins { get; set; }
        //public List<SnackisUser> Members { get; set; }
        //public List<SnackisUser> BlockedUsers { get; set; }
        //public List<IdentityRole> Roles { get; set; }


        [BindProperty(SupportsGet = true)]
        public string RemoveUserId { get; set; }

        [BindProperty(SupportsGet = true)]
        public string AddUserId { get; set; }

        [BindProperty(SupportsGet = true)]
        public string Role { get; set; }

        public bool IsLastAdmin { get; set; }
        public bool MemberIsAdmin { get; set; }


        public async Task<IActionResult> OnGetAsync()
        {
            //UserIsAdmin = await _userManager.IsInRoleAsync(CurrentUser, "Admin");
            //IsMember = await _userManager.IsInRoleAsync(CurrentUser, "Medlem");

            //Roles = _roleManager.Roles.OrderBy(r => r.Name).ToList();
            //Users = _userManager.Users.OrderBy(u => u.UserName).ToList();

            IList<SnackisUser> adminSearch = await _userManager.GetUsersInRoleAsync("Admin");
            int numberOfAdmins = adminSearch.Count();

            IsLastAdmin = false;
            MemberIsAdmin = false;

            if (RemoveUserId != null)
            {
                if (Role == "Admin")
                {
                    if (numberOfAdmins <= 1)
                    {
                        IsLastAdmin = true;
                        return Page();
                    }
                }

                if (Role == "Medlem")
                {
                    MemberIsAdmin = _userManager.GetUsersInRoleAsync("Admin").Result.ToList().FirstOrDefault(u => u.Id == RemoveUserId) != null;

                    if (MemberIsAdmin)
                    {
                        return Page();
                    }

                }

                SnackisUser user = await _userManager.FindByIdAsync(RemoveUserId);
                IdentityResult result = await _userManager.RemoveFromRoleAsync(user, Role);

            }

            if (AddUserId != null)
            {
                SnackisUser user = await _userManager.FindByIdAsync(AddUserId);

                if (Role == "Admin" /*&& userIsMember == false*/)
                {
                    IdentityResult result = await _userManager.AddToRoleAsync(user, "Medlem"); //Funkar även om user redan är Medlem, vilket är det vanliga fallet
                    result = await _userManager.AddToRoleAsync(user, Role);
                }

                else
                {
                    IdentityResult result = await _userManager.AddToRoleAsync(user, Role);
                }
            }

            return Page();
        }
    }
}
