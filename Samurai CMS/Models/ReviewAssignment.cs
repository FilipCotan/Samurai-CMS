using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Samurai_CMS.Models
{
    public class ReviewAssignment
    {
        [Key, Column(Order = 0), ForeignKey("User")]
        public string UserId { get; set; }

        [Key, Column(Order = 1), ForeignKey("Paper")]
        public int PaperId { get; set; }

        public virtual User User { get; set; }

        public virtual AuthorPaper Paper { get; set; }

        public string BiddingResult { get; set; }

        public string ReviewQualifier { get; set; }

        [StringLength(400)]
        public string Recommendations { get; set; }
    }
}