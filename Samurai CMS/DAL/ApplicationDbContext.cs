using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Samurai_CMS.Models;

namespace Samurai_CMS.DAL
{
    public class ApplicationDbContext : IdentityDbContext<User>
    {
        public DbSet<Role> UserRoles { get; set; }

        public DbSet<Conference> Conferences { get; set; }

        public DbSet<Edition> Editions { get; set; }

        public DbSet<Session> Sessions { get; set; }

        public DbSet<AuthorPaper> AuthorPapers { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
    
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<Samurai_CMS.Models.Enrollment> Enrollments { get; set; }
    }
}