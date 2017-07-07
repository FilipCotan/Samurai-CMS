using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using Samurai_CMS.Models;

namespace Samurai_CMS.ViewModels
{
    public class ReviewPaperViewModel
    {
        public AuthorPaper Paper { get; set; }

        [StringLength(400)]
        public string Recommendations { get; set; }

        public bool IsAccepted { get; set; }
    }
}