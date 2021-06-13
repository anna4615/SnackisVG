using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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

        [BindProperty]
        public List<IFormFile> UploadedImages { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var user = await _userManager.GetUserAsync(User);

            string checkedTitle = await _offensiveWordsGateway.GetCheckedText(Post.Title);
            string checkedText = await _offensiveWordsGateway.GetCheckedText(Post.Text);

            // spara bild till wwwroot/postimg
           

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

            //if (UploadedImages != null)
            //{
            //    foreach (var image in UploadedImages)
            //    {
            //        Post.Images.Add(image.FileName);
            //    }
            //}

            Post createdPost = await _postGateway.PostPost(Post);

            if (UploadedImages != null)
            {
                foreach (IFormFile file in UploadedImages)
                {

                await _postGateway.PostPostImage(new PostImage
                {
                    PostId = createdPost.Id,
                    FileName = file.FileName
                });

                    string fileLocation = $"./wwwroot/postimg/{file.FileName}";
                    using (FileStream fileStream = new FileStream(fileLocation, FileMode.Create))
                    {
                        await file.CopyToAsync(fileStream);
                    }
                }
            }

            return Redirect($"/Discussion?SubjectId={SubjectId}");
        }
    }
}
