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
            Post deletePost = allPosts.FirstOrDefault(p => p.Id == DeleteOffensivePostId || p.Id == DeletePostId);

            // När man tar bort en post raderas även dess svar
            if (deletePost.Posts != null)
            {
                foreach (var postlevel1 in deletePost.Posts)
                {
                    if (postlevel1.Posts != null)
                    {
                        foreach (var postlevel2 in postlevel1.Posts)
                        {
                            if (postlevel2.Posts != null)
                            {
                                foreach (var postlevel3 in postlevel2.Posts)
                                {
                                    if (postlevel3.Posts != null)
                                    {
                                        foreach (var postlevel4 in postlevel3.Posts)
                                        {
                                            if (postlevel4.Posts != null)
                                            {
                                                List<Post> answers5 = postlevel4.Posts;
                                                foreach (var postlevel5 in postlevel4.Posts)
                                                {
                                                    await _postGateway.DeletePost(postlevel5.Id);
                                                }
                                            }
                                            await _postGateway.DeletePost(postlevel4.Id);
                                        }
                                    }
                                    await _postGateway.DeletePost(postlevel3.Id);
                                }
                            }
                            await _postGateway.DeletePost(postlevel2.Id);
                        }
                    }
                    await _postGateway.DeletePost(postlevel1.Id);
                }
            }
            await _postGateway.DeletePost(deletePost.Id);

            // Om man deletar ett anmält inlägg laddas sidan med lista över anmälda inlägg efter delete
            if (DeleteOffensivePostId != 0)
            {
                return Redirect("./OffensivePosts");
            }

            // Om man deletar av någon annan anledning laddas sidan med inlägg för ämnet efter delete
            if (DeletePostId != 0)
            {
                return Redirect($"./PostsView?SubjectId={deletePost.SubjectId}");
            }

            return Page();
        }
    }
}
