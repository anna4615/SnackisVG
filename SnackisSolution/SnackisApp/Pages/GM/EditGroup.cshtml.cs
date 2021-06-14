using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SnackisApp.Areas.Identity.Data;
using SnackisApp.Data;
using Microsoft.EntityFrameworkCore;
using SnackisApp.Models;

namespace SnackisApp.Pages.GM
{
    public class EditGroupModel : PageModel
    {
        private readonly UserManager<SnackisUser> _userManager;
        private readonly SnackisContext _context;

        public EditGroupModel(
            UserManager<SnackisUser> userManager,
            SnackisContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public int GroupId { get; set; }

        [BindProperty]
        public List<string> NewMemberNames { get; set; }
        public List<string> UserNames { get; set; }



        public async Task<IActionResult> OnGetAsync()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            UserNames = await GetMemberNames();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            foreach (var name in NewMemberNames)
            {
                await AddMemberToGroup(name);
            }

            return Redirect("./index");
        }



        private async Task<List<string>> GetMemberNames()
        {
            List<SnackisUser> allUsers = _userManager.Users
               .OrderBy(u => u.UserName)
               .ToList();

            Group group = _context.Group.Include(g => g.Members).FirstOrDefault(g => g.Id == 1);
            //Group group = _context.Group.Include(g => g.Members).FirstOrDefault(g => g.Id == GroupId);
            List<SnackisUser> existingMembers = group.Members.ToList();

            var userNames = new List<string>();

            foreach (var user in allUsers)
            {
                bool isForumMember = await _userManager.IsInRoleAsync(user, "Medlem");
                bool notInGroup = existingMembers.FirstOrDefault(m => m.Id == user.Id) == null;

                if (isForumMember && notInGroup)
                {
                    userNames.Add(user.UserName);
                }
            }

            return userNames;
        }


        private async Task AddMemberToGroup(string newMemberName)
        {
            Group group = _context.Group.Include(g => g.Members).FirstOrDefault(g => g.Id == 1);
            //Group group = _context.Group.Include(g => g.Members).FirstOrDefault(g => g.Id == GroupId);

            SnackisUser newMember = _userManager.Users.FirstOrDefault(u => u.UserName == newMemberName);

            group.Members.Add(newMember);
            await _context.SaveChangesAsync();

        }
    }
}
