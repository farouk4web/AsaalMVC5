using Asaal.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Asaal.Models
{
    public class SiteEmail
    {
        [EmailAddress(ErrorMessageResourceName = "emailNotValid", ErrorMessageResourceType = typeof(ViewsKeys))]
        [Display(Name = "email", ResourceType = typeof(ViewsKeys))]
        [Required(ErrorMessageResourceName = "required", ErrorMessageResourceType = typeof(ViewsKeys))]
        public string Email { get; set; }
         
        [DataType(DataType.Password)]
        [Display(Name = "password", ResourceType = typeof(ViewsKeys))]
        [Required(ErrorMessageResourceName = "required", ErrorMessageResourceType = typeof(ViewsKeys))]
        public string Password { get; set; }
    }
}