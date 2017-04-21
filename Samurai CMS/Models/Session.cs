using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Samurai_CMS.Models
{
    public class Session
    {
        public int Id { get; set; }

        [Required]
        [StringLength(300)]
        public string Topic { get; set; }

        public bool? IsMorningSession { get; set; }

        public int EditionId { get; set; }

        public virtual Edition Edition { get; set; }

        public string UserId { get; set; }

        public virtual User User { get; set; }
        
        public virtual ICollection<AuthorPaper> Papers { get; set; } = new HashSet<AuthorPaper>();
    }
}