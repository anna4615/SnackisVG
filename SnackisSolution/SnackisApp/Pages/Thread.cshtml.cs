using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SnackisApp.Areas.Identity.Data;
using SnackisApp.Gateways;
using SnackisApp.Models;

namespace SnackisApp.Pages
{
    public class ThreadModel : PageModel
    {
        private readonly UserManager<SnackisUser> _userManager;
        private readonly ISubjectGateway _subjectGateway;
        private readonly IPostGateway _postGateway;

        public ThreadModel(UserManager<SnackisUser> userManager,
            ISubjectGateway subjectGateway, IPostGateway postGateway)
        {
            _userManager = userManager;
            _subjectGateway = subjectGateway;
            _postGateway = postGateway;
        }

        public Post StartPost { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PostId { get; set; }

        public async Task<IActionResult> OnGet()
        {
            StartPost = await _postGateway.GetPost(PostId);

            return Page();
        }

    }
}
