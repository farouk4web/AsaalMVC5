using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asaal.DTOs
{
    public class VoteDto
    {
        public int AnswerId { get; set; }

        public bool IsGoodAnswerOrNot { get; set; }
    }
}