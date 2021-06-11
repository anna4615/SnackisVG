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
using SnackisApp.Gateways;

namespace SnackisApp.Pages.MI
{
    public class MyMIModel : PageModel
    {
        private readonly UserManager<SnackisUser> _userManager;
        private readonly SnackisContext _context;
        private readonly IOffensiveWordsGateway _offensiveWordsGateway;

        public MyMIModel(
            UserManager<SnackisUser> userManager,
            SnackisContext context,
            IOffensiveWordsGateway offensiveWordsGateway)
        {
            _userManager = userManager;
            _context = context;
            _offensiveWordsGateway = offensiveWordsGateway;
        }

        [BindProperty]
        public MemberInfo MemberInfo { get; set; }


        public void OnGet()
        {

        }

        public async Task<IActionResult> OnPostAsync()
        {
            var currentUser = await _userManager.GetUserAsync(User);

            //Kolla om medlemmen har MI sedan tidigare
            MemberInfo userMI = _context.MemberInfo.Where(mi => mi.UserId == currentUser.Id).FirstOrDefault();

            string checkedText = await _offensiveWordsGateway.GetCheckedText(MemberInfo.Text);

            if (userMI == null)
            {
                MemberInfo.UserId = currentUser.Id;
                MemberInfo.Text = checkedText;
                await _context.AddAsync(MemberInfo);
                await _context.SaveChangesAsync();
            }

            else
            {
                userMI.Text = checkedText;                
                await _context.SaveChangesAsync();
            }

            return RedirectToPage();
        }
    }
}
