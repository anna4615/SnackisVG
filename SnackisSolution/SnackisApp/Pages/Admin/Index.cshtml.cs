using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SnackisApp.Gateways;
using SnackisApp.Models;

namespace SnackisApp.Pages.Admin
{
    public class IndexModel : PageModel
    {
        private readonly IForumGateway _forumGateway;

        public IndexModel(IForumGateway forumGateway)
        {
            _forumGateway = forumGateway;
        }

        [BindProperty]
        public Forum NewForum { get; set; }

        public Forum Forum { get; set; }

        [BindProperty]
        public Subject Subject { get; set; }

        public List<Subject> Subjects { get; set; }

        public bool EditAllowed { get; set; }


        public async Task<IActionResult> OnGetAsync()
        {
            List<Forum> forums = await _forumGateway.GetForums();

            if (forums.Count != 0)
            {
                Forum = forums.FirstOrDefault();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (NewForum.Name != null)
            {
                await _forumGateway.PostForum(NewForum);
            }            

            return RedirectToPage("./index");
        }
    }
}
