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
    public class IndexModel : PageModel
    {
        private readonly UserManager<SnackisUser> _userManager;
        private readonly SnackisContext _context;

        public IndexModel(
            UserManager<SnackisUser> userManager,
            SnackisContext context)
        {
            _userManager = userManager;
            _context = context;
        }


        public List<Group> MyGroups { get; set; }
        public List<Group> InvitedToGroups { get; set; }
        public List<Membership> InvitedToMemberships { get; set; }
        public List<Group> AcceptedGroups { get; set; }
        public List<Membership> AcceptedMemberships { get; set; }


        [BindProperty(SupportsGet = true)]
        public int AddMembershipId { get; set; }

        [BindProperty(SupportsGet = true)]
        public int DeleteMembershipId { get; set; }


        public List<SnackisUser> Members { get; set; }

        [BindProperty]
        public Group SelectedGroup { get; set; }



        public async Task<IActionResult> OnGetAsync()
        {
            if (AddMembershipId != 0)
            {
                Membership membership = await _context.Membership.FindAsync(AddMembershipId);
                membership.IsAccepted = true;
                await _context.SaveChangesAsync();
            }

            if (DeleteMembershipId != 0)
            {
                Membership membership = await _context.Membership.FindAsync(DeleteMembershipId);
                _context.Membership.Remove(membership);
                await _context.SaveChangesAsync();
            }

            List<Group> allGroups = _context.Group.Include(g => g.Memberships).ToList();
            SnackisUser currentUser = await _userManager.GetUserAsync(User);
            List<Membership> allMemberships = await _context.Membership.ToListAsync();

            MyGroups = allGroups.Where(g => g.UserId == currentUser.Id).ToList();

            InvitedToMemberships = allMemberships.Where(ms => ms.UserId == currentUser.Id && ms.IsAccepted == false).ToList();
            InvitedToGroups = new List<Group>();
            foreach (Membership ms in InvitedToMemberships)
            {
                Group group = allGroups.FirstOrDefault(g => g.Id == ms.GroupId);
                InvitedToGroups.Add(group);
            }

            AcceptedMemberships = allMemberships.Where(ms => ms.UserId == currentUser.Id && ms.IsAccepted).ToList();
            AcceptedGroups = new List<Group>();
            foreach (Membership ms in AcceptedMemberships)
            {
                Group group = allGroups.FirstOrDefault(g => g.Id == ms.GroupId);
                AcceptedGroups.Add(group);
            }

            await _context.SaveChangesAsync();

      

            return Page();
        }



        public async Task<IActionResult> OnPostAsync()
        {



            return Redirect("");
        }
    }
}
