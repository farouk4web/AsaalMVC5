using Asaal.Models;
using Asaal.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Asaal.ViewModels
{
    public class SiteSettingVM
    {
        public SiteDomain SiteDomain { get; set; }

        public SiteEmail SiteEmail{ get; set; }

        [Required(ErrorMessageResourceName = "required", ErrorMessageResourceType = typeof(ViewsKeys))]
        public bool SendEmailOrNot { get; set; }
    }
}