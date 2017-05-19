using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Samurai_CMS.Models
{
    [Serializable]
    [Flags]
    public enum Roles
    {
        Administrator = 0, 
        Chair,
        CoChair,
        ProgramCommitteeMember,
        Author,
        Listener
    }
}