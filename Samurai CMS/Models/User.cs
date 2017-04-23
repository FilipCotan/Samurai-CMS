using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;


namespace Samurai_CMS.Models
{
    public class User : IdentityUser
    {
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        public string WebsiteUrl { get; set; }

        public int? RoleId { get; set; }

        public virtual UserRole Role { get; set; }

        public virtual ICollection<Enrollment> Editions { get; set; } = new HashSet<Enrollment>();

        public virtual ICollection<ReviewAssignment> ReviewAssignments { get; set; } = new HashSet<ReviewAssignment>();

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }
}