using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SnackisApp.Gateways;
using SnackisApp.Models;

namespace SnackisApp.Pages.Admin.ForumAdmin
{
    public class OffensivePostsModel : PageModel
    {
        private readonly ISubjectGateway _subjectGateway;
        private readonly IPostGateway _postGateway;

        public OffensivePostsModel(ISubjectGateway subjectGateway, IPostGateway postGateway)
        {
            _subjectGateway = subjectGateway;
            _postGateway = postGateway;
        }

        public List<Post> OffensivePosts { get; set; }

        [BindProperty(SupportsGet = true)]
        public int DeletePostId { get; set; }

        [BindProperty(SupportsGet = true)]
        public int AllowPostId { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var allPosts = await _postGateway.GetPosts();

            OffensivePosts = allPosts.Where(p => p.IsOffensiv == true).ToList();

            if (AllowPostId != 0)
            {
                Post post = OffensivePosts.Where(p => p.Id == AllowPostId).FirstOrDefault();
                post.IsOffensiv = false;
                await _postGateway.PutPost(AllowPostId, post);

                return RedirectToPage();
            }

            return Page();
        }      
    }
}
