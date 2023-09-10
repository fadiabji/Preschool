using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Preschool.Models;

namespace Preschool.Data
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Child> Childern { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<Class> Classes  { get; set; }

        public DbSet<DocumentsImage> DocumentsImages { get; set; }

    }

}