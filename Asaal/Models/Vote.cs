using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Asaal.Models
{
    public class Vote
    {
        public int Id { get; set; }

        [Required]
        public bool IsGoodAnswer { get; set; }

        [Required]
        public int AnswerId { get; set; }
        public virtual Answer Answer { get; set; }

        public string UserId { get; set; }
        public virtual ApplicationUser User { get; set; }
    }
}