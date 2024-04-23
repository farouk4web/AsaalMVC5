using Asaal.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Asaal.Models
{
    public class Answer
    {
        public int Id { get; set; }

        [Display(Name = "content", ResourceType = typeof(ViewsKeys))]
        [Required(ErrorMessageResourceName = "required", ErrorMessageResourceType = typeof(ViewsKeys))]
        [StringLength(1000, ErrorMessageResourceName = "stringLength5_1000", ErrorMessageResourceType = typeof(ViewsKeys), MinimumLength = 5)]
        public string Content { get; set; }

        [Display(Name = "publishDate", ResourceType = typeof(ViewsKeys))]
        public DateTime PublishDate { get; set; }

        public int QuestionId { get; set; }
        public virtual Question Question { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }

        public virtual ICollection<Replay> Replays { get; set; }

        public virtual ICollection<Vote> Votes { get; set; }
    }
}