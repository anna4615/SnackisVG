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

namespace SnackisApp.Pages.PM
{
    public class ViewPMModel : PageModel
    {
        private readonly UserManager<SnackisUser> _userManager;
        private readonly SnackisContext _context;

        public ViewPMModel(
            UserManager<SnackisUser> userManager,
            SnackisContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        public List<PrivateMessage> SentPMs { get; set; }
        public List<PrivateMessage> ReceivedPMs { get; set; }

        public async Task<IActionResult> OnGet()
        {
            SnackisUser currentUser = await _userManager.GetUserAsync(User);

            SentPMs = _context.PrivateMessage
                .Where(m => m.UserId == currentUser.Id)
                .OrderByDescending(m => m.Date)
                .ToList();

            ReceivedPMs = _context.PrivateMessage
                .Where(m => m.ToUserName == currentUser.UserName)
                .OrderByDescending(m => m.Date)
                .ToList();


            return Page();
        }
    }
}
