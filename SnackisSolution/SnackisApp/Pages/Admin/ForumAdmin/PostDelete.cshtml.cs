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
    public class PostDeleteModel : PageModel
    {
        private readonly IPostGateway _postGateway;

        public PostDeleteModel(IPostGateway postGateway)
        {
            _postGateway = postGateway;
        }

        [BindProperty(SupportsGet = true)]
        public int DeletePostId { get; set; }

        [BindProperty(SupportsGet = true)]
        public int DeleteOffensivePostId { get; set; }

        //[BindProperty(SupportsGet = true)]
        //public int DeletePostId { get; set; }


        public Post Post { get; set; }

        public async Task<IActionResult> OnGet()
        {
            if (DeletePostId != 0)
            {
                Post = await _postGateway.GetPost(DeletePostId);
            }

            if (DeleteOffensivePostId != 0)
            {
                Post = await _postGateway.GetPost(DeleteOffensivePostId);
            }

            return Page();
        }

        public async Task<IActionResult> OnPost()
        {
            List<Post> allPosts = await _postGateway.GetPosts();

            // N�r man tar bort en post deletes �ven dess svar
            List<Post> answers = allPosts.Where(p => p.PostId == DeleteOffensivePostId || p.PostId == DeletePostId).ToList();
            foreach (var post in answers)
            {
                await _postGateway.DeletePost(post.Id);
            }

            // Om man deletar ett anm�lt inl�gg laddas sidan med lista �ver anm�lda inl�gg efter delete
            if (DeleteOffensivePostId != 0)
            {
                var post = await _postGateway.DeletePost(DeleteOffensivePostId);
                return Redirect("./OffensivePosts");
            }

            // Om man deletar av n�gon annan anledning laddas sidan med inl�gg f�r �mnet efter delete
            if (DeletePostId != 0)
            {

                var post = await _postGateway.DeletePost(DeletePostId);
                return Redirect($"./PostsView?SubjectId={post.SubjectId}");
            }

            return Page();
        }
    }
}
