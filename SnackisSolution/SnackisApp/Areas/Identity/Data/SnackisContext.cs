using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SnackisApp.Areas.Identity.Data;
using SnackisApp.Models;

namespace SnackisApp.Data
{
    public class SnackisContext : IdentityDbContext<SnackisUser>
    {
        public SnackisContext(DbContextOptions<SnackisContext> options)
            : base(options)
        {
        }

        public DbSet<PrivateMessage> PrivateMessage { get; set; }
        public DbSet<MemberInfo> MemberInfo { get; set; }

        public DbSet<Group> Group { get; set; }
        public DbSet<Membership> Membership { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);
        }
    }
}
