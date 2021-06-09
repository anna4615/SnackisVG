using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SnackisApp.Gateways;
using SnackisApp.Models;

namespace SnackisApp.Pages
{
    public class ReportModel : PageModel
    {
        private readonly IPostGateway _postGateway;

        public ReportModel(IPostGateway postGateway)
        {
            _postGateway = postGateway;
        }


        [BindProperty(SupportsGet = true)]
        public int ReportPostId { get; set; }

        public Post Post { get; set; }



        public async Task<IActionResult> OnGetAsync()
        {
            Post = await _postGateway.GetPost(ReportPostId);

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            Post = await _postGateway.GetPost(ReportPostId);

            Post.IsOffensiv = true;

            await _postGateway.PutPost(ReportPostId, Post);

            return Page();           
        }
    }
}
