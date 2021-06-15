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

        public List<SnackisUser> Members { get; set; }

        [BindProperty]
        public Group SelectedGroup { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            MyGroups = _context.Group.Where(g => g.UserId == currentUser.Id).ToList(); // Include Members orsakade krash "A possible object cycle was detected"
            //List<Group> gwithm = _context.Group.Include(g => g.Members).Where(g => g.UserId == currentUser.Id).ToList();

            

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var members = new List<SnackisUser>();



            return Redirect("");
        }
    }
}
