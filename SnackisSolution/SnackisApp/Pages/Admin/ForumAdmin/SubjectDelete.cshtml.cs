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
    public class SubjectDeleteModel : PageModel
    {
        private readonly ISubjectGateway _subjectGateway;

        public SubjectDeleteModel(ISubjectGateway subjectGateway)
        {
            _subjectGateway = subjectGateway;
        }

        [BindProperty(SupportsGet = true)]
        public int SubjectDeleteId { get; set; }

        [BindProperty]
        public Subject Subject { get; set; }


        public async Task<IActionResult> OnGetAsync()
        {
            Subject = await _subjectGateway.GetSubject(SubjectDeleteId);

            if (Subject == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _subjectGateway.DeleteSubject(SubjectDeleteId);

            return RedirectToPage("./Index");
        }
    }
}
