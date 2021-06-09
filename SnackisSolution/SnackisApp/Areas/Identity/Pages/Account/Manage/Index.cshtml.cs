using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SnackisApp.Areas.Identity.Data;

namespace SnackisApp.Areas.Identity.Pages.Account.Manage
{
    public partial class IndexModel : PageModel
    {
        private readonly UserManager<SnackisUser> _userManager;
        private readonly SignInManager<SnackisUser> _signInManager;

        public IndexModel(
            UserManager<SnackisUser> userManager,
            SignInManager<SnackisUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        public string Username { get; set; }

        [TempData]
        public string StatusMessage { get; set; }

        [BindProperty]
        public InputModel Input { get; set; }

        [BindProperty]
        public IFormFile UploadedImage { get; set; }

        public class InputModel
        {
            [Phone]
            [Display(Name = "Telefon")]
            public string PhoneNumber { get; set; }

            [Display(Name = "Bild")]
            public string Picture { get; set; }
        }

        private async Task LoadAsync(SnackisUser user)
        {
            var userName = await _userManager.GetUserNameAsync(user);
            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            var picture = user.Picture;

            Username = userName;

            Input = new InputModel
            {
                PhoneNumber = phoneNumber,
                Picture = picture
            };
        }

        public async Task<IActionResult> OnGetAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            await LoadAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            if (!ModelState.IsValid)
            {
                await LoadAsync(user);
                return Page();
            }

            var phoneNumber = await _userManager.GetPhoneNumberAsync(user);
            if (Input.PhoneNumber != phoneNumber)
            {
                var setPhoneResult = await _userManager.SetPhoneNumberAsync(user, Input.PhoneNumber);
                if (!setPhoneResult.Succeeded)
                {
                    StatusMessage = "Unexpected error when trying to set phone number.";
                    return RedirectToPage();
                }
            }

            if (UploadedImage != null && UploadedImage.FileName != user.Picture)
            {
                string file = "./wwwroot/img/" + UploadedImage.FileName;
                using (FileStream fileStream = new FileStream(file, FileMode.Create))
                {
                    await UploadedImage.CopyToAsync(fileStream);
                }

                string deletePicture = user.Picture;

                user.Picture = UploadedImage.FileName;
                await _userManager.UpdateAsync(user);

                // Delete image if no one else uses it
                if (_userManager.Users.Where(u => u.Picture == deletePicture).FirstOrDefault() == null)
                {
                    System.IO.File.Delete($"./wwwroot/img/{deletePicture}");
                }

            }


            await _signInManager.RefreshSignInAsync(user);
            StatusMessage = "Din profil är uppdaterad.";
            return RedirectToPage();
        }
    }
}
