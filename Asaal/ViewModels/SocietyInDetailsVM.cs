using Asaal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asaal.ViewModels
{
    public class SocietyInDetailsVM
    {
        public Society Society { get; set; }

        public IEnumerable<Answer> LastAnswersInSociety { get; set; }

        public IEnumerable<ApplicationUser> BestUsersInSociety { get; set; }
    }
}