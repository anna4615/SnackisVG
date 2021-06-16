using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SnackisApp.Gateways;
using SnackisApp.Models;

namespace SnackisApp.Pages
{
    public class EditPostModel : PageModel
    {
        private readonly IPostGateway _postGateway;

        public EditPostModel(IPostGateway postGateway)
        {
            _postGateway = postGateway;
        }

        [BindProperty(SupportsGet = true)]
        public int LikePostId { get; set; }

        [BindProperty(SupportsGet = true)]
        public int LovePostId { get; set; }



        public async Task<IActionResult> OnGetAsync()
        {
            Post post = new Post();

            if (LikePostId != 0)
            {
                post = await _postGateway.GetPost(LikePostId);
                post.NumberOfLike++;
                await _postGateway.PutPost(LikePostId, post);
            }

            if (LovePostId != 0)
            {
                post = await _postGateway.GetPost(LovePostId);
                post.NumberOfLove++;
                await _postGateway.PutPost(LovePostId, post);
            }

            Post startPost = new Post();

            if (post.PostId != null)
            {
                int id = (int)post.PostId;
                startPost = await _postGateway.GetStartPostId(id);
            }
            else
            {
                startPost = post;
            }

            if (startPost.GroupId == null)
            {
                return Redirect($"/Thread?PostId={startPost.Id}");
            }
            else
            {
                return Redirect($"/GM/GMThread?PostId={startPost.Id}");
            }
        }



    }
}
