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
        public DbSet<UserRole> UserRoles { get; set; }

        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }
    
        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }
    }
}