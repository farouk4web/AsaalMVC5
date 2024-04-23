using Asaal.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Asaal.Models
{
    public class SiteDomain
    {
        [Url]
        [Display(Name = "siteDomain", ResourceType = typeof(ViewsKeys))]
        [Required(ErrorMessageResourceName = "required", ErrorMessageResourceType = typeof(ViewsKeys))]
        public string SiteUrl { get; set; }
    }
}