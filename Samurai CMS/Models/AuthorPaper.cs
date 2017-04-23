using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Samurai_CMS.Models
{
    public class AuthorPaper
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Authors { get; set; }

        public string Keywords { get; set; }

        public bool? IsAccepted { get; set; }

        public string AbstractFileType { get; set; }

        public byte[] Abstract { get; set; }

        public string PaperFileType { get; set; }

        public byte[] Paper { get; set; }

        public int SessionId { get; set; }

        public virtual Session Session { get; set; }

        public virtual ICollection<ReviewAssignment> ReviewAssignments { get; set; } = new HashSet<ReviewAssignment>();
    }
}