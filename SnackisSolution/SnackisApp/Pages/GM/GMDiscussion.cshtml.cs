using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using SnackisApp.Areas.Identity.Data;
using SnackisApp.Data;
using SnackisApp.Gateways;
using SnackisApp.Models;

namespace SnackisApp.Pages.GM
{
    public class GMDiscussionModel : PageModel
    {
        private readonly UserManager<SnackisUser> _userManager;
        private readonly ISubjectGateway _subjectGateway;
        private readonly IPostGateway _postGateway;
        private readonly SnackisContext _context;

        public GMDiscussionModel(
            UserManager<SnackisUser> userManager,
            ISubjectGateway subjectGateway,
            IPostGateway postGateway,
            SnackisContext context)
        {
            _userManager = userManager;
            _subjectGateway = subjectGateway;
            _postGateway = postGateway;
            _context = context;
        }

        [BindProperty(SupportsGet = true)]
        public int GroupId { get; set; }

        public List<Post> AllPosts { get; set; }

        public Group Group { get; set; }

        public List<Post> ParentPosts { get; set; }

        public List<Membership> Memberships { get; set; }


        public async Task<IActionResult> OnGetAsync()
        {
            Group = _context.Group.FirstOrDefault(g => g.Id == GroupId);
            //Memberships används för att hindra utomstående att gå in på sidan genom att ange ett annat grupp Id i url:en.
            Memberships = _context.Membership.Where(ms => ms.GroupId == Group.Id).ToList();
            AllPosts = await _postGateway.GetPosts();

            ParentPosts = AllPosts.Where(p => p.GroupId == GroupId && p.PostId == null).ToList();

            return Page();
        }
    }
}
