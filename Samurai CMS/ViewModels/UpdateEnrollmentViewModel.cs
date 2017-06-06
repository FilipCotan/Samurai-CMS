using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Samurai_CMS.Models;

namespace Samurai_CMS.ViewModels
{
    public class UpdateEnrollmentViewModel
    {
        public int EditionId { get; set; }

        public int? PaperId { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        public string Authors { get; set; }

        [Required]
        public string Keywords { get; set; }

        public HttpPostedFileBase Abstract { get; set; }

        public HttpPostedFileBase Paper { get; set; }
    }
}