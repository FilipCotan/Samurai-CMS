using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Web;

namespace Samurai_CMS.Models
{
    public class Enrollment
    {
        [Key, Column(Order = 0), ForeignKey("User")]
        public string UserId { get; set; }

        [Key, Column(Order = 1), ForeignKey("Edition")]
        public int EditionId { get; set; }

        public virtual User User { get; set; }

        public int? RoleId { get; set; }

        public virtual Role Role { get; set; }

        public virtual Edition Edition { get; set; }

        public int? PaperId { get; set; }

        public virtual AuthorPaper Paper { get; set; }

        public string Affiliation { get; set; }
    }
}