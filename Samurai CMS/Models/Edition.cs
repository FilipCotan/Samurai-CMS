using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Samurai_CMS.Models
{
    public class Edition
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(30)]
        public string Title { get; set; }

        [Required]
        [MaxLength(600)]
        public string Description { get; set; }

        [Required]
        [Range(2017, 2100, ErrorMessage = "The year must be between {0} and {1}")]
        public int Year { get; set; }

        [DisplayName("Abstract Deadline")]        
        public DateTime? AbstractDeadline { get; set; }

        [DisplayName("Paper Deadline")]
        public DateTime? PaperDeadline { get; set; }

        [DisplayName("Results Date")]
        public DateTime? ResultsDeadline { get; set; }

        [DisplayName("Start Date")]
        public DateTime? StartDate { get; set; }

        [DisplayName("End Date")]
        public DateTime? EndDate { get; set; }

        [DisplayName("Conference")]
        public int ConferenceId { get; set; }

        public virtual Conference Conference { get; set; }

        public virtual ICollection<Session> Sessions { get; set; } = new HashSet<Session>();

        public virtual ICollection<Enrollment> Participants { get; set; }  = new HashSet<Enrollment>();
    }
}