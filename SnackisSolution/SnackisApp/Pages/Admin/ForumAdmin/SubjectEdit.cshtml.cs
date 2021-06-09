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
    public class SubjectEditModel : PageModel
    {
        private readonly ISubjectGateway _subjectGateway;
        private readonly IPostGateway _postGateway;

        public SubjectEditModel(ISubjectGateway subjectGateway)
        {
            _subjectGateway = subjectGateway;
        }

        [BindProperty(SupportsGet = true)]
        public int SubjectEditId { get; set; }

        [BindProperty]
        public Subject Subject { get; set; }


        public async Task<IActionResult> OnGetAsync()
        {
            Subject = await _subjectGateway.GetSubject(SubjectEditId);

            if (Subject == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _subjectGateway.PutSubject(SubjectEditId, Subject);

            return RedirectToPage("./Index");
        }
    }
}
