using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SnackisApp.Areas.Identity.Data;
using SnackisApp.Data;
using SnackisApp.Models;

namespace SnackisApp.Pages.MI
{
    public class ViewMIModel : PageModel
    {
        private readonly UserManager<SnackisUser> _userManager;
        private readonly SnackisContext _context;

        public ViewMIModel(
            UserManager<SnackisUser> userManager,
            SnackisContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public string ShowUserName { get; set; }

        public MemberInfo MemberInfo { get; set; }

        public List<string> MembersOnly { get; set; }

        public SnackisUser ShowUser { get; set; }


        public async Task<IActionResult> OnGetAsync()
        {

            if (ShowUserName != null)
            {
                ShowUser = _userManager.Users
              .FirstOrDefault(u => u.UserName == ShowUserName);

                MemberInfo = _context.MemberInfo
                    .FirstOrDefault(mi => mi.UserId == ShowUser.Id);
            }

            List<SnackisUser> allUsers = _userManager.Users
               .OrderBy(u => u.UserName)
               .ToList();

            MembersOnly = new List<string>();

            foreach (var user in allUsers)
            {
                bool isMember = await _userManager.IsInRoleAsync(user, "Medlem");

                if (isMember)
                {
                    MembersOnly.Add(user.UserName);
                }
            }

            return Page();
        }

       public async Task OnPostAsync()
        {  
            ShowUser = _userManager.Users
                .FirstOrDefault(u => u.UserName == ShowUserName);

            MemberInfo = _context.MemberInfo
                .FirstOrDefault(mi => mi.UserId == ShowUser.Id);

            List<SnackisUser> allUsers = _userManager.Users
              .OrderBy(u => u.UserName)
              .ToList();

            MembersOnly = new List<string>();

            foreach (var user in allUsers)
            {
                bool isMember = await _userManager.IsInRoleAsync(user, "Medlem");

                if (isMember)
                {
                    MembersOnly.Add(user.UserName);
                }
            }
        }
    }
}
