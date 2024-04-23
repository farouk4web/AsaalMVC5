using Asaal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asaal.ViewModels
{
    public class UserProfileVM
    {
        public ApplicationUser User { get; set; }

        public ChangePasswordViewModel ChangePassword { get; set; }

        public UpdateProfile UpdateProfile { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsSupervisor { get; set; }
    }
}