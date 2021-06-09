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
    public class PostsModel : PageModel
    {
        private readonly UserManager<SnackisUser> _userManager;
        private readonly ISubjectGateway _subjectGateway;
        private readonly IPostGateway _postGateway;

        public PostsModel(UserManager<SnackisUser> userManager,
            ISubjectGateway subjectGateway, IPostGateway postGateway)
        {
            _userManager = userManager;
            _subjectGateway = subjectGateway;
            _postGateway = postGateway;
        }

        public List<SnackisUser> Users { get; set; }

        //public string UserName { get; set; }
        public List<Post> Answers { get; set; }
        public Post ParentPost { get; set; }
        public Subject Subject { get; set; }

        [BindProperty(SupportsGet = true)]
        public int PostId { get; set; }


        public async Task<IActionResult> OnGetAsync()
        {
            Users = _userManager.Users.ToList();

            var posts = await _postGateway.GetPosts();

            ParentPost = posts.Where(p => p.Id == PostId).FirstOrDefault();
            Answers = posts.Where(p => p.PostId == ParentPost.Id).ToList();

            Subject = await _subjectGateway.GetSubject(ParentPost.SubjectId);

            return Page();
        }
    }
}
