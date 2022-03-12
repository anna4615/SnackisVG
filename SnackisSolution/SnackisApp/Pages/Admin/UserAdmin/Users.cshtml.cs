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
        public bool MemberIsOriginalAdmin { get; set; }
        public bool MemberIsStina { get; set; }


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
                SnackisUser user = await _userManager.FindByIdAsync(RemoveUserId);

                //Not possible to remove admin rights from admin account
                if (Role == "Admin" && user.UserName == "admin")
                {
                    MemberIsOriginalAdmin = true;
                    return Page();
                }

                //Not possible to remove admin rights from stinas account
                if (Role == "Admin" && user.UserName == "stina")
                {
                    MemberIsStina = true;
                    return Page();
                }

                //At least admin and one additional user must have role "Admin"
                if (Role == "Admin")
                {
                    if (numberOfAdmins <= 2)
                    {
                        IsLastAdmin = true;
                        return Page();
                    }
                }

                //Not possible to remove the role "Medlem" from Admin
                if (Role == "Medlem")
                {
                    MemberIsAdmin = _userManager.GetUsersInRoleAsync("Admin").Result.ToList().FirstOrDefault(u => u.Id == RemoveUserId) != null;

                    if (MemberIsAdmin)
                    {
                        return Page();
                    }

                }

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
