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
            Group.Members = new List<SnackisUser>();

            foreach (var name in MemberNames)
            {
                AddMemberToGroup(Group, name);
            }

            await _context.AddAsync(Group);
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

        private void AddMemberToGroup(Group group, string memberName)
        {
            //Group group = _context.Group.Include(g => g.Members).FirstOrDefault(g => g.Id == 2);
            //Group group = _context.Group.Include(g => g.Members).FirstOrDefault(g => g.Id == GroupId);

            SnackisUser member = _userManager.Users.FirstOrDefault(u => u.UserName == memberName);

            group.Members.Add(member);
           // await _context.SaveChangesAsync();

        }
    }
}
