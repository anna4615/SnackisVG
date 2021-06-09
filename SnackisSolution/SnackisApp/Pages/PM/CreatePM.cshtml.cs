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
    public class CreatePMModel : PageModel
    {
        private readonly UserManager<SnackisUser> _userManager;
        private readonly SnackisContext _context;

        public CreatePMModel(
            UserManager<SnackisUser> userManager,
            SnackisContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [BindProperty]
        public PrivateMessage Message { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PMId { get; set; }

        [BindProperty(SupportsGet = true)]
        public string ForUser { get; set; }


        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            Message.UserId = currentUser.Id;
            Message.Date = DateTime.UtcNow;
            if (PMId != 0)
            {
                Message.PrivateMessageId = PMId;
            }
            //if (ForUser != null)
            //{
            //    Message.ToUserName = ForUser;
            //}

            _context.Add(Message);
            _context.SaveChanges();

            return RedirectToPage("/PM/ViewPM");
        }
    }
}
