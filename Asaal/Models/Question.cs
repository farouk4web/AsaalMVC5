using Asaal.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Asaal.Models
{
    public class Question
    {
        public int Id { get; set; }

        [Display(Name = "title", ResourceType = typeof(ViewsKeys))]
        [Required(ErrorMessageResourceName = "required", ErrorMessageResourceType = typeof(ViewsKeys))]
        [StringLength(250, ErrorMessageResourceName = "stringLength20_250", ErrorMessageResourceType = typeof(ViewsKeys), MinimumLength = 20)]
        public string Title { get; set; }

        [Display(Name = "content", ResourceType = typeof(ViewsKeys))]
        [Required(ErrorMessageResourceName = "required", ErrorMessageResourceType = typeof(ViewsKeys))]
        [StringLength(1000, ErrorMessageResourceName = "stringLength100_1000", ErrorMessageResourceType = typeof(ViewsKeys), MinimumLength = 100)]
        public string Content { get; set; }

        [Display(Name = "publishDate", ResourceType = typeof(ViewsKeys))]
        [Required(ErrorMessageResourceName = "required", ErrorMessageResourceType = typeof(ViewsKeys))]
        public DateTime PublishDate { get; set; }

        [Display(Name = "views", ResourceType = typeof(ViewsKeys))]
        [Required(ErrorMessageResourceName = "required", ErrorMessageResourceType = typeof(ViewsKeys))]
        public int Views { get; set; }

        [Display(Name = "society", ResourceType = typeof(ViewsKeys))]
        [Required(ErrorMessageResourceName = "required", ErrorMessageResourceType = typeof(ViewsKeys))]
        public int SocietyId { get; set; }
        public virtual Society Society { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }

        //public IEnumerable<string> Tags { get; set; }
    }
}