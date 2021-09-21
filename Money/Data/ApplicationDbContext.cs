using Microsoft.EntityFrameworkCore;
using Money.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Money.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options):base(options)
        {

        }

        public DbSet<Category> Category { get; set; }
        public DbSet<ApplicationType> applicationTypes { get; set; }

    }
}
