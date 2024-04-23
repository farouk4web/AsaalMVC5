using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asaal.Models
{
    public static class RoleName
    {
        public const string Admins = "Admins";
        public const string Supervisors = "Supervisors";
        public const string AdminsAndSupervisors = Admins + "," + Supervisors;
    }
}