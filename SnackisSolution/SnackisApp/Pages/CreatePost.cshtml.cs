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
    public class CreatePostModel : PageModel
    {
        private readonly UserManager<SnackisUser> _userManager;
        private readonly IPostGateway _postGateway;
        private readonly IOffensiveWordsGateway _offensiveWordsGateway;

        public CreatePostModel(
            UserManager<SnackisUser> userManager,
            IPostGateway postGateway,
            IOffensiveWordsGateway offensiveWordsGateway)
        {
            _userManager = userManager;
            _postGateway = postGateway;
            _offensiveWordsGateway = offensiveWordsGateway;
        }

        public List<Post> Posts { get; set; }

        [BindProperty(SupportsGet = true)]
        public int SubjectId { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PostId { get; set; }

        [BindProperty]
        public Post Post { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            string checkedTitle = await _offensiveWordsGateway.GetCheckedText(Post.Title);
            string checkedText = await _offensiveWordsGateway.GetCheckedText(Post.Text);

            Post.UserId = user.Id;
            Post.SubjectId = SubjectId;

            if (PostId != 0)
            {
                Post.PostId = PostId;
            }

            if (string.IsNullOrWhiteSpace(Post.Title))
            {
                Post.Title = "-----";
            }
            else
            {
            Post.Title = checkedTitle;
            }

            Post.Text = checkedText;
            Post.Date = DateTime.UtcNow;
            Post.IsOffensiv = false;

            await _postGateway.PostPost(Post);

            //if (PostId != 0)
            //{
            //    return Redirect($"/Thread?PostId={PostId}");
            //}

                return Redirect($"/Discussion?SubjectId={SubjectId}");
        }
    }
}
