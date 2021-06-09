using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SnackisApp.Areas.Identity.Data;
using SnackisApp.Gateways;

namespace SnackisApp.Pages
{
    public class SeedingModel : PageModel
    {

        private readonly HelpMethods.Content _content;

        public SeedingModel(
            HelpMethods.Content content)
        {
            _content = content;
        }

        public async Task<IActionResult> OnGet()
        {
            await _content.SeedUsers();
            await _content.SeedForum();
            await _content.SeedSubjects();
            await _content.SeedPosts();

            return RedirectToPage("/index");
        }
    }
}
