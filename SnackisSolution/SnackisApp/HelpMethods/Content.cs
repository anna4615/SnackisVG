using Microsoft.AspNetCore.Identity;
using SnackisApp.Areas.Identity.Data;
using SnackisApp.Gateways;
using SnackisApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SnackisApp.HelpMethods
{
    public class Content
    {
        private readonly IForumGateway _forumGateway;
        private readonly ISubjectGateway _subjectGateway;
        private readonly IPostGateway _postGateway;
        private readonly UserManager<SnackisUser> _userManager;

        public Content(
            IForumGateway forumGateway,
            ISubjectGateway subjectGateway,
            IPostGateway postGateway,
            UserManager<SnackisUser> userManager)
        {
            _forumGateway = forumGateway;
            _subjectGateway = subjectGateway;
            _postGateway = postGateway;
            _userManager = userManager;
        }

        public static List<SnackisUser> CreateUsers()
        {
            List<SnackisUser> users = new List<SnackisUser>();

            users.Add(new SnackisUser
            {
                Email = "admin@a.se",
                UserName = "admin",
                FirstName = "Anna",
                LastName = "Sjöberg",
                Picture = "default.png"
            });

            users.Add(new SnackisUser
            {
                Email = "n@n.se",
                UserName = "nisse",
                FirstName = "Nils",
                LastName = "Ny",
                Picture = "spade-24434_1280.png"
            });

            users.Add(new SnackisUser
            {
                Email = "g@g.se",
                UserName = "goran",
                FirstName = "Göran",
                LastName = "Gro",
                Picture = "gardening-5233862_1280.png"
            });

            users.Add(new SnackisUser
            {
                Email = "s@s.se",
                UserName = "stina",
                FirstName = "Stina",
                LastName = "Så",
                Picture = "ladybug-476344_1280.png"
            });

            users.Add(new SnackisUser
            {
                Email = "f@f.se",
                UserName = "flora",
                FirstName = "Flora",
                LastName = "Frö",
                Picture = "spring-flower-4165109_1280.png"
            });

            return users;
        }

        public static Forum CreateForum()
        {
            Forum forum = new Forum();
            forum.Name = "Trädgårdssnack";

            return forum;
        }

        public static List<Subject> CreateSubjectList(Forum forum)
        {
            List<Subject> subjects = new List<Subject>();

            subjects.Add(new Subject
            {
                ForumId = forum.Id,
                Name = "Köksträdgård"
            });

            subjects.Add(new Subject
            {
                ForumId = forum.Id,
                Name = "Perenner"
            });

            subjects.Add(new Subject
            {
                ForumId = forum.Id,
                Name = "Sommarblommor"
            });

            subjects.Add(new Subject
            {
                ForumId = forum.Id,
                Name = "Gräsmatta"
            });

            subjects.Add(new Subject
            {
                ForumId = forum.Id,
                Name = "Träd och buskar"
            });

            return subjects;
        }

        public static List<Post> CreateParentPostsList(List<SnackisUser> users, List<Subject> subjects)
        {
            List<Post> posts = new List<Post>();

            posts.Add(new Post
            {
                UserId = users.ElementAt(Utils.GetRandomNumber(users.Count)).Id,
                SubjectId = subjects.ElementAt(0).Id,
                Date = DateTime.UtcNow,
                Title = "Goda morötter",
                Text = "Kan någon ge mig tips om goda morotssorter?",
                IsOffensiv = false
            });

            posts.Add(new Post
            {
                UserId = users.ElementAt(Utils.GetRandomNumber(users.Count)).Id,
                SubjectId = subjects.ElementAt(0).Id,
                Date = DateTime.UtcNow,
                Title = "Potatisskörd",
                Text = "Hur skall jag göra för att få potatisen klar för skörd till midsommar?",
                IsOffensiv = false
            });

            posts.Add(new Post
            {
                UserId = users.ElementAt(Utils.GetRandomNumber(users.Count)).Id,
                SubjectId = subjects.ElementAt(1).Id,
                Date = DateTime.UtcNow,
                Title = "Jord till stjärnflocka",
                Text = "Donec pulvinar pharetra mauris quis consequat. Nunc quis libero vitae massa pellentesque aliquam vitae eu diam. Interdum et malesuada fames ac ante ipsum primis in faucibus. Donec in diam auctor, sagittis tellus at, tincidunt ipsum. Phasellus vel lacinia lacus. Aenean gravida tortor at nunc viverra, vitae elementum erat volutpat. Ut non tortor eget risus vehicula ultrices. Nullam cursus imperdiet est. Etiam velit tellus, blandit ut vulputate at, finibus porta orci. Nullam dignissim eleifend magna nec dignissim. Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Mauris nec lobortis ex.",
                IsOffensiv = false
            });

            return posts;
        }

        public static List<Post> CreateAnswers(List<SnackisUser> users, List<Post> parentPosts)
        {
            List<Post> posts = new List<Post>();

            posts.Add(new Post
            {
                UserId = users.ElementAt(Utils.GetRandomNumber(users.Count)).Id,
                PostId = parentPosts.ElementAt(0).Id,
                SubjectId = parentPosts.ElementAt(0).SubjectId,
                Date = DateTime.UtcNow,
                Text = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Morbi consequat magna sed nulla vestibulum, ut porta diam vestibulum. Praesent molestie lacinia tincidunt. Phasellus sit amet orci tellus. Nulla ultricies ligula diam. Cras risus velit, egestas at tincidunt eget, tempor et elit. Integer in laoreet ipsum, vel egestas urna.",
                IsOffensiv = false
            });

            posts.Add(new Post
            {
                UserId = users.ElementAt(Utils.GetRandomNumber(users.Count)).Id,
                PostId = parentPosts.ElementAt(0).Id,
                SubjectId = parentPosts.ElementAt(0).SubjectId,
                Date = DateTime.UtcNow,
                Text = "Nullam diam lectus, consectetur ac auctor ut, blandit a risus. Sed eu dui non lacus porttitor rhoncus non vel purus. Sed vulputate eu urna ac tristique.",
                IsOffensiv = false
            });

            posts.Add(new Post
            {
                UserId = users.ElementAt(Utils.GetRandomNumber(users.Count)).Id,
                PostId = parentPosts.ElementAt(1).Id,
                SubjectId = parentPosts.ElementAt(1).SubjectId,
                Date = DateTime.UtcNow,
                Text = "Orci varius natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Cras iaculis nisi sit amet justo vehicula, eu laoreet lectus mattis.",
                IsOffensiv = false
            });

            return posts;
        }

        public async Task SeedUsers()
        {
            List<SnackisUser> users = CreateUsers();

            foreach (var user in users)
            {
                var result = await _userManager.CreateAsync(user, "Zxcv!234");

                if (user.UserName == "admin")
                {
                    await _userManager.AddToRoleAsync(user, "Admin");
                }
                await _userManager.AddToRoleAsync(user, "Medlem");

            }
        }

        public async Task SeedForum()
        {
            Forum forum = CreateForum();

            await _forumGateway.PostForum(forum);
        }

        public async Task SeedSubjects()
        {
            List<Forum> forums = await _forumGateway.GetForums();
            Forum forum = forums.FirstOrDefault();

            var subjects = CreateSubjectList(forum);

            foreach (var subject in subjects)
            {
                await _subjectGateway.PostSubject(subject);
            }
        }

        public async Task SeedPosts()
        {
            List<SnackisUser> users = _userManager.Users.ToList();
            List<Subject> subjects = await _subjectGateway.GetSubjects();

            List<Post> parentPosts = CreateParentPostsList(users, subjects);

            foreach (var post in parentPosts)
            {
                await _postGateway.PostPost(post);
            }

            parentPosts = await _postGateway.GetPosts();

            List<Post> answers = CreateAnswers(users, parentPosts);

            foreach (var post in answers)
            {
                await _postGateway.PostPost(post);
            }
        }
    }
}
