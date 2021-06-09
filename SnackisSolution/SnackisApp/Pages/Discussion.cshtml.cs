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
    public class DiscussionModel : PageModel
    {
        private readonly UserManager<SnackisUser> _userManager;
        private readonly ISubjectGateway _subjectGateway;
        private readonly IPostGateway _postGateway;

        public DiscussionModel(UserManager<SnackisUser> userManager,
            ISubjectGateway subjectGateway, IPostGateway postGateway)
        {
            _userManager = userManager;
            _subjectGateway = subjectGateway;
            _postGateway = postGateway;
        }

        public Subject Subject { get; set; }
        public List<Post> AllPosts { get; set; }
        public List<Post> ParentPosts { get; set; }

        [BindProperty(SupportsGet = true)]
        public int SubjectId { get; set; }

        public int NumerOfAnswers { get; set; }


        public async Task<IActionResult> OnGetAsync()
        {
            Subject = await _subjectGateway.GetSubject(SubjectId);
            AllPosts = await _postGateway.GetPosts();

            ParentPosts = AllPosts.Where(p => p.SubjectId == SubjectId && p.PostId == null).ToList();

            return Page();
        }
    }
}
