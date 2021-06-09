using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using SnackisApp.Areas.Identity.Data;
using SnackisApp.Gateways;
using SnackisApp.Models;

namespace SnackisApp.Areas.Identity.Pages.Account.Manage
{
    public class DeletePersonalDataModel : PageModel
    {
        private readonly UserManager<SnackisUser> _userManager;
        private readonly SignInManager<SnackisUser> _signInManager;
        private readonly ILogger<DeletePersonalDataModel> _logger;
        private readonly IPostGateway _postGateway;

        public DeletePersonalDataModel(
            UserManager<SnackisUser> userManager,
            SignInManager<SnackisUser> signInManager,
            ILogger<DeletePersonalDataModel> logger,
            IPostGateway postGateway)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;
            _postGateway = postGateway;
        }

       
        [BindProperty]
        public InputModel Input { get; set; }

        public class InputModel
        {
            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }
        }

        public bool RequirePassword { get; set; }

        public async Task<IActionResult> OnGet()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            RequirePassword = await _userManager.HasPasswordAsync(user);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);
            if (user == null)
            {
                return NotFound($"Unable to load user with ID '{_userManager.GetUserId(User)}'.");
            }

            RequirePassword = await _userManager.HasPasswordAsync(user);
            if (RequirePassword)
            {
                if (!await _userManager.CheckPasswordAsync(user, Input.Password))
                {
                    ModelState.AddModelError(string.Empty, "Felaktigt lösenord.");
                    return Page();
                }

                if (await _userManager.IsInRoleAsync(user, "Admin"))
                {
                    ModelState.AddModelError(string.Empty, "Användaren har administratörsrättigheter och kan därför inte raderas. Ta bort adminrättigheter och försök igen.");
                    return Page();
                }
            }
            
            

            //// Ta bort alla poster som användaren skrivit + svar
            //var allPosts = await _postGateway.GetPosts();
            //var usersPosts = allPosts.Where(p => p.UserId == user.Id).ToList();

            //var answers = new List<Post>();

            //foreach (var post in usersPosts)
            //{
            //    var answersToPost = allPosts.Where(p => p.PostId == post.Id).ToList();
            //    answers.AddRange(answersToPost);
            //}

            //foreach (var post in answers)
            //{
            //    await _postGateway.DeletePost(post.Id);
            //}

            //foreach (var post in usersPosts)
            //{
            //    await _postGateway.DeletePost(post.Id);
            //}


            string deletePicture = user.Picture;
            var result = await _userManager.DeleteAsync(user);

            // ta bort användarbild om den inte är defaultbilden och ingen annan använder samma bild
            if (user.Picture != "default.png" && _userManager.Users.Where(u => u.Picture == deletePicture).FirstOrDefault() == null)
            {
                System.IO.File.Delete($"./wwwroot/img/{deletePicture}");
            }


            var userId = await _userManager.GetUserIdAsync(user);
            if (!result.Succeeded)
            {
                throw new InvalidOperationException($"Unexpected error occurred deleting user with ID '{userId}'.");
            }

            await _signInManager.SignOutAsync();

            _logger.LogInformation("User with ID '{UserId}' deleted themselves.", userId);

            return Redirect("~/");
        }
    }
}
