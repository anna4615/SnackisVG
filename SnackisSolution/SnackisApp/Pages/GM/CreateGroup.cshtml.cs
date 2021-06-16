using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using SnackisApp.Areas.Identity.Data;
using SnackisApp.Data;
using SnackisApp.Models;

namespace SnackisApp.Pages.GM
{
    public class CreateGroupModel : PageModel
    {
        private readonly UserManager<SnackisUser> _userManager;
        private readonly SnackisContext _context;

        public CreateGroupModel(
            UserManager<SnackisUser> userManager,
            SnackisContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [BindProperty]
        public Group Group { get; set; }

        [BindProperty]
        public List<string> MemberNames { get; set; }
        public List<string> UserNames { get; set; }



        public async Task<IActionResult> OnGetAsync()
        {
            UserNames = await GetUserNames();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            Group.UserId = currentUser.Id;
            Group.Memberships = new List<Membership>();

            await _context.AddAsync(Group);
            await _context.SaveChangesAsync(); // SaveChanges för att få gruppens Id

            foreach (var name in MemberNames)
            {
                AddMembershipToGroup(Group, name);
            }

            await _context.SaveChangesAsync();

            return Redirect("./index");
        }


        private async Task<List<string>> GetUserNames()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            List<SnackisUser> allUsers = _userManager.Users
               .OrderBy(u => u.UserName)
               .ToList();

            var userNames = new List<string>();

            // Lägger inte till blockade users till selectlist för grupp medlemmar
            foreach (var user in allUsers)
            {
                bool isForumMember = await _userManager.IsInRoleAsync(user, "Medlem");
                bool isCurrentUser = currentUser.UserName == user.UserName;

                if (isForumMember && isCurrentUser == false)
                {
                    userNames.Add(user.UserName);
                }
            }

            return userNames;
        }

        private void AddMembershipToGroup(Group group, string memberName)
        {
            SnackisUser member = _userManager.Users.FirstOrDefault(u => u.UserName == memberName);

            Membership membership = new Membership
            {
                UserId = member.Id,
                GroupId = group.Id,
                IsAccepted = false
            };

            group.Memberships.Add(membership);
        }
    }
}
