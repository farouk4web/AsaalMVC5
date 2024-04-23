using Asaal.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Asaal.Models
{
    public class Society
    {
        public int Id { get; set; }

        [Display(Name = "name", ResourceType = typeof(ViewsKeys))]
        [Required(ErrorMessageResourceName = "required", ErrorMessageResourceType = typeof(ViewsKeys))]
        [StringLength(100, ErrorMessageResourceName = "stringLength4_100", ErrorMessageResourceType = typeof(ViewsKeys), MinimumLength = 4)]
        public string Name { get; set; }

        [Display(Name= "description", ResourceType = typeof(ViewsKeys))]
        [Required(ErrorMessageResourceName = "required", ErrorMessageResourceType = typeof(ViewsKeys))]
        [StringLength(500, ErrorMessageResourceName = "stringLength150_500", ErrorMessageResourceType = typeof(ViewsKeys), MinimumLength = 150)]
        public string Description { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}