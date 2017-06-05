using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Samurai_CMS.Models;

namespace Samurai_CMS.ViewModels
{
    public class EnrollmentViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Authors { get; set; }

        [Required]
        public string Keywords { get; set; }

        public bool? IsAccepted { get; set; }

        [Required]
        public HttpPostedFileBase Abstract { get; set; }

        public HttpPostedFileBase Paper { get; set; }

        public int SessionId { get; set; }

        [Required]
        public string Affiliation { get; set; }

        public virtual Session Session { get; set; }
    }
}