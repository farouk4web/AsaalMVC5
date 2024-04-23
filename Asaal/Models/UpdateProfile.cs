using Asaal.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Asaal.Models
{
    public class UpdateProfile
    {
        [Required(ErrorMessageResourceName = "required", ErrorMessageResourceType = typeof(ViewsKeys))]
        [MinLength(4, ErrorMessageResourceName = "min4", ErrorMessageResourceType = typeof(ViewsKeys)), MaxLength(50, ErrorMessageResourceName = "max50", ErrorMessageResourceType = typeof(ViewsKeys))]
        [RegularExpression("^[a-zA-Zء-ي ]*$", ErrorMessageResourceName = "vlaidFullNameMsg", ErrorMessageResourceType = typeof(ViewsKeys))]
        [Display(Name = "fullName", ResourceType = typeof(ViewsKeys))]
        public string FullName { get; set; }

        [Required(ErrorMessageResourceName = "required", ErrorMessageResourceType = typeof(ViewsKeys))]
        [Display(Name = "country", ResourceType = typeof(ViewsKeys))]
        public string CountryCode { get; set; }
    }
}