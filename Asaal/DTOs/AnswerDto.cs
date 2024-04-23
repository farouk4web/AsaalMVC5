using Asaal.Resources;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Asaal.DTOs
{
    public class AnswerDto
    {
        public int Id { get; set; }

        [Display(Name = "content", ResourceType = typeof(ViewsKeys))]
        [Required(ErrorMessageResourceName = "required", ErrorMessageResourceType = typeof(ViewsKeys))]
        [StringLength(1000, ErrorMessageResourceName = "stringLength5_1000", ErrorMessageResourceType = typeof(ViewsKeys), MinimumLength = 5)]
        public string Content { get; set; }

        public DateTime PublishDate { get; set; }

        public int QuestionId { get; set; }

        public string UserId { get; set; }

        public string UserFullName { get; set; }

        public string UserProfileImageSrc { get; set; }
    }
}