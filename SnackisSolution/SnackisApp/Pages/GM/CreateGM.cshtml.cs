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

namespace SnackisApp.Pages.GM
{
    public class CreateGMModel : PageModel
    {
        private readonly UserManager<SnackisUser> _userManager;
        private readonly IPostGateway _postGateway;
        private readonly ISubjectGateway _subjectGateway;

        public CreateGMModel(
            UserManager<SnackisUser> userManager,
            IPostGateway postGateway,
            ISubjectGateway subjectGateway)
        {
            _userManager = userManager;
            _postGateway = postGateway;
            _subjectGateway = subjectGateway;
        }

        public List<Post> Posts { get; set; }

        [BindProperty(SupportsGet = true)]
        public int GroupId { get; set; }

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

            //Posten måste ha ett SubjectId eftersom SubjectId inte är nullable, subject "Gruppmeddelande måste alltid finnas, skall skapas när Forumet skapas.
            Subject subject = _subjectGateway.GetSubjects().Result.FirstOrDefault(s => s.Name == "Gruppmeddelande");

            Post.UserId = user.Id;
            Post.GroupId = GroupId;
            Post.SubjectId = subject.Id;

            if (PostId != 0)
            {
                Post.PostId = PostId;
            }

            if (string.IsNullOrWhiteSpace(Post.Title))
            {
                Post.Title = "-----";
            }

            Post.Date = DateTime.UtcNow;

            Post createdPost = await _postGateway.PostPost(Post);

            // spara bild till wwwroot/postimg          

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

            Post startPost = new Post();

            if (createdPost.PostId != null)
            {
                int id = (int)createdPost.PostId;
                startPost = await _postGateway.GetStartPostId(id);
            }
            else
            {
                startPost = createdPost;
            }


            return Redirect($"/GM/GMThread?PostId={startPost.Id}");
        }
    }
}
