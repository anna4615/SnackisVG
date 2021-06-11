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
    public class IndexModel : PageModel
    {
        private readonly IOffensiveWordsGateway _wordsGateway;

        public IndexModel(
            IOffensiveWordsGateway wordsGateway)
        {
            _wordsGateway = wordsGateway;
        }

        [BindProperty]
        public OffensiveWord OffensiveWord { get; set; }

        public List<OffensiveWord> Words { get; set; }


        public async Task<IActionResult> OnGetAsync()
        {
            Words = await _wordsGateway.GetWords();
            Words = Words.OrderBy(w => w.Word).ToList();

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (OffensiveWord.Word != null)
            {
                OffensiveWord.Word = OffensiveWord.Word.ToLower();
                await _wordsGateway.PostWord(OffensiveWord);
            }

            Words = await _wordsGateway.GetWords();

            return RedirectToPage("./index");
        }
    }
}
