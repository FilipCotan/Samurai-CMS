using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Samurai_CMS.Models
{
    public class Conference
    {
        public int Id { get; set; }

        [MaxLength(20)]
        public string Name { get; set; }

        [MaxLength(40)]
        public string Theme { get; set; }

        [Display(Name="Paper is required")]
        public bool PaperIsRequired { get; set; }

        [Display(Name = "Number of reviewers")]
        public int ReviewersCount { get; set; }

        public virtual ICollection<Edition> Editions { get; set; } = new HashSet<Edition>();
    }
}