using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PostsAPI.Models;

namespace PostsAPI.Data
{
    public class SnackisContext : DbContext
    {
        public SnackisContext (DbContextOptions<SnackisContext> options)
            : base(options)
        {
        }

        public DbSet<PostsAPI.Models.Subject> Subject { get; set; }

        public DbSet<PostsAPI.Models.Post> Post { get; set; }

        public DbSet<PostsAPI.Models.Forum> Forum { get; set; }


    }
}
