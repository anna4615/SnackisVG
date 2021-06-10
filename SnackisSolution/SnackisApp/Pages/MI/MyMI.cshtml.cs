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
    public class MyMIModel : PageModel
    {
        private readonly UserManager<SnackisUser> _userManager;
        private readonly SnackisContext _context;

        public MyMIModel(
            UserManager<SnackisUser> userManager,
            SnackisContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [BindProperty]
        public MemberInfo MemberInfo { get; set; }


        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            var userMI = _context.MemberInfo.Where(mi => mi.UserId == currentUser.Id).FirstOrDefault();

            if (userMI == null)
            {
                MemberInfo.UserId = currentUser.Id;
                await _context.AddAsync(MemberInfo);
                await _context.SaveChangesAsync();
            }

            else
            {
                userMI.Text = MemberInfo.Text;                
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}
