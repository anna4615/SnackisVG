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
    public class PostsViewModel : PageModel
    {
        private readonly ISubjectGateway _subjectGateway;
        private readonly IPostGateway _postGateway;

        public PostsViewModel(ISubjectGateway subjectGateway, IPostGateway postGateway)
        {
            _subjectGateway = subjectGateway;
            _postGateway = postGateway;
        }

        [BindProperty(SupportsGet = true)]
        public int SubjectId { get; set; }

        public Subject Subject { get; set; }
        public List<Post> Posts { get; set; }


        public async Task<IActionResult> OnGetAsync()
        {
            Subject = await _subjectGateway.GetSubject(SubjectId);
            List<Post> posts = await _postGateway.GetPosts();

            Posts = posts.Where(p => p.SubjectId == SubjectId)
                .OrderBy(p => p.Date)
                .ToList();

            return Page();
        }
    }
}
