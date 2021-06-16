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

        public List<string> MemberNames { get; set; }  //till selectlist för DeleteMemmbers
        public List<string> NotInGroupNames { get; set; } // till selectlist för AddMembers

        public Group SelectedGroup { get; set; }


        public async Task<IActionResult> OnGetAsync()
        {
            SelectedGroup = await _context.Group.Include(g => g.Memberships).FirstOrDefaultAsync(g => g.Id == GroupEditId);

            MemberNames = new List<string>(); ;

            foreach (var membership in SelectedGroup.Memberships)
            {
                SnackisUser user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == membership.UserId);
                if (membership.UserId != SelectedGroup.UserId)
                {
                    MemberNames.Add(user.UserName);  //Skaparen av gruppen skall inte vara med på listan av members som kan tas bort
                }
            }

            NotInGroupNames = await GetUserNames(SelectedGroup);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            SelectedGroup = await _context.Group.Include(g => g.Memberships).FirstOrDefaultAsync(g => g.Id == GroupEditId);

            if (AddMemberNames.Count != 0)
            {
                foreach (var name in AddMemberNames)
                {
                    AddMembershipToGroup(SelectedGroup, name);
                }
            }

            if (DeleteMemberNames.Count != 0)
            {
                foreach (var name in DeleteMemberNames)
                {
                    await DeleteMembershipFromGroup(SelectedGroup, name);
                }
            }

            await _context.SaveChangesAsync();

            return Redirect("./index");
        }


        // Hämta Username för users som inte redan är med i gruppen
        private async Task<List<string>> GetUserNames(Group group)
        {
            var currentUser = await _userManager.GetUserAsync(User);
            List<SnackisUser> members = new List<SnackisUser>();


            foreach (var membership in group.Memberships)
            {
                SnackisUser user = await _userManager.Users.FirstOrDefaultAsync(u => u.Id == membership.UserId);
                members.Add(user);
            }

            List<SnackisUser> allUsers = _userManager.Users
               .OrderBy(u => u.UserName)
               .ToList();


            var userNames = new List<string>();

            foreach (var user in allUsers)
            {
                bool isForumMember = await _userManager.IsInRoleAsync(user, "Medlem");
                bool notInGroup = members.FirstOrDefault(m => m.Id == user.Id) == null;
                bool isCurrentUser = currentUser.UserName == user.UserName; // Skaparen av gruppen skall inte vara med på listan

                if (isForumMember && notInGroup && isCurrentUser == false)
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

        private async Task DeleteMembershipFromGroup(Group group, string memberName)
        {
            SnackisUser member = _userManager.Users.FirstOrDefault(u => u.UserName == memberName);

            var membership = await _context.Membership.FirstOrDefaultAsync(m => m.UserId == member.Id && m.GroupId == group.Id);

            group.Memberships.Remove(membership);
        }
    }
}
