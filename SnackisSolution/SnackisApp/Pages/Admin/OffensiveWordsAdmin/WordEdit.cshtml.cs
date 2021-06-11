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
    public class WordEditModel : PageModel
    {
        private readonly IOffensiveWordsGateway _wordGateway;

        public WordEditModel(
            IOffensiveWordsGateway wordGateway)
        {
            _wordGateway = wordGateway;
        }

        [BindProperty(SupportsGet = true)]
        public int WordEditId { get; set; }

        [BindProperty]
        public OffensiveWord ForbiddenWord { get; set; }


        public async Task<IActionResult> OnGetAsync()
        {
            ForbiddenWord = await _wordGateway.GetWord(WordEditId);

            if (ForbiddenWord == null)
            {
                return NotFound();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            await _wordGateway.PutWord(WordEditId, ForbiddenWord);

            return RedirectToPage("./Index");
        }
    }
}
