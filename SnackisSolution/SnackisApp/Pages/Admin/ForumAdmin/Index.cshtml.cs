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
    public class IndexModel : PageModel
    {
        private readonly IForumGateway _forumGateway;
        private readonly ISubjectGateway _subjectGateway;

        public IndexModel(IForumGateway forumGateway, ISubjectGateway subjectGateway)
        {
            _forumGateway = forumGateway;
            _subjectGateway = subjectGateway;
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
            Subjects = await _subjectGateway.GetSubjects();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (Subject.Name != null)
            {
                List<Forum> forums = await _forumGateway.GetForums();
                Forum = forums.FirstOrDefault();
                Subject.ForumId = Forum.Id;
                await _subjectGateway.PostSubject(Subject);
            }

            Subjects = await _subjectGateway.GetSubjects();

            return RedirectToPage("./index");
        }
    }
}
