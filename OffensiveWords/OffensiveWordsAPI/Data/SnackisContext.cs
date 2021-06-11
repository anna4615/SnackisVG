using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using OffensiveWordsAPI.Models;

namespace OffensiveWordsAPI.Data
{
    public class SnackisContext : DbContext
    {
        public SnackisContext (DbContextOptions<SnackisContext> options)
            : base(options)
        {
        }

        public DbSet<OffensiveWordsAPI.Models.OffensiveWord> OffensiveWord { get; set; }
    }
}
