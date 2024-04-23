using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asaal.Models
{   
    public class Notification
    {
        public int Id { get; set; }

        [Required]
        public string Content { get; set; }

        public DateTime CreatedDate { get; set; }

        public int AnswerId { get; set; }
        public virtual Answer Answer { get; set; }

        public int QuestionId { get; set; }

        public string TargetUserId { get; set; }
        public virtual ApplicationUser TargetUser { get; set; }
    }
}