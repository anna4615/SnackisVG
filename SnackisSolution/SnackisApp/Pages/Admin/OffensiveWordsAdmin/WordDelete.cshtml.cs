using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SnackisApp.Gateways;
using SnackisApp.Models;

namespace SnackisApp.Pages.Admin.OffensiveWordsAdmin
{
    public class WordDeleteModel : PageModel
    {
        private readonly IOffensiveWordsGateway _wordsGateway;

        public WordDeleteModel(IOffensiveWordsGateway wordsGateway)
        {
            _wordsGateway = wordsGateway;
        }

        [BindProperty(SupportsGet = true)]
        public int WordDeleteId { get; set; }

        [BindProperty]
        public OffensiveWord ForbiddenWord { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            ForbiddenWord = await _wordsGateway.GetWord(WordDeleteId);

            if (ForbiddenWord == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _wordsGateway.DeleteWord(WordDeleteId);

            return RedirectToPage("./Index");
        }


    }
}
