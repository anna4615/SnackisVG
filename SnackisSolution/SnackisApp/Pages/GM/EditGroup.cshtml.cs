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
        public int GroupEditId { get; set; }

        [BindProperty]
        public List<string> AddMemberNames { get; set; }

        [BindProperty]
        public List<string> DeleteMemberNames { get; set; }

        public List<string> MemberNames { get; set; }
        public List<string> NotInGroupNames { get; set; }

        public Group SelectedGroup { get; set; }
        public string GroupName { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            SelectedGroup = await _context.Group.Include(g => g.Members).FirstOrDefaultAsync(g => g.Id == GroupEditId);
            GroupName = SelectedGroup.Name;
            MemberNames = SelectedGroup.Members.Select(m => m.UserName).ToList();

            NotInGroupNames = await GetNewUserNames();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (AddMemberNames.Count != 0)
            {
                foreach (var name in AddMemberNames)
                {
                    await AddMemberToGroup(name);
                }
            }

            if (DeleteMemberNames.Count != 0)
            {
                foreach (var name in DeleteMemberNames)
                {
                    await DeleteMemberFromGroup(name);
                }
            }

            return Redirect("./index");
        }       


        // Hämta Username för users som inte redan är med i gruppen
        private async Task<List<string>> GetNewUserNames()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            List<SnackisUser> allUsers = _userManager.Users
               .OrderBy(u => u.UserName)
               .ToList();

            Group group = _context.Group.Include(g => g.Members).FirstOrDefault(g => g.Id == GroupEditId);
            List<SnackisUser> existingMembers = group.Members.ToList();

            var userNames = new List<string>();

            foreach (var user in allUsers)
            {
                bool isForumMember = await _userManager.IsInRoleAsync(user, "Medlem");
                bool notInGroup = existingMembers.FirstOrDefault(m => m.Id == user.Id) == null;
                bool isCurrentUser = currentUser.UserName == user.UserName;

                if (isForumMember && notInGroup && isCurrentUser == false)
                {
                    userNames.Add(user.UserName);
                }
            }

            return userNames;
        }


        private async Task AddMemberToGroup(string newMemberName)
        {
            Group group = await _context.Group.Include(g => g.Members).FirstOrDefaultAsync(g => g.Id == GroupEditId);

            SnackisUser addMember = _userManager.Users.FirstOrDefault(u => u.UserName == newMemberName);

            group.Members.Add(addMember);
            await _context.SaveChangesAsync();

        }

        private async Task DeleteMemberFromGroup(string deleteMemberName)
        {
            Group group = await _context.Group.Include(g => g.Members).FirstOrDefaultAsync(g => g.Id == GroupEditId);

            SnackisUser deleteMember = _userManager.Users.FirstOrDefault(u => u.UserName == deleteMemberName);

            group.Members.Remove(deleteMember);
            await _context.SaveChangesAsync();
        }
    }
}
