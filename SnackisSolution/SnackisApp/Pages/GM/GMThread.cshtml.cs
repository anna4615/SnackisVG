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

namespace SnackisApp.Pages.GM
{
    public class GMThreadModel : PageModel
    {
        private readonly IPostGateway _postGateway;

        public GMThreadModel( 
            IPostGateway postGateway)
        {
            _postGateway = postGateway;
        }

        public Post StartPost { get; set; }


        [BindProperty(SupportsGet = true)]
        public int PostId { get; set; }



        public async Task<IActionResult> OnGetAsync()
        {
            StartPost = await _postGateway.GetPost(PostId);

            return Page();
        }
    }
}
