using Asaal.DTOs;
using Asaal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asaal.ViewModels
{
    public class QuestionInDetailsVM
    {
        public Question Question{ get; set; }

        public AnswerDto AnswerDto { get; set; }

        public ReplayDto ReplayDto { get; set; }

        public IEnumerable<Question> HighViewQuestion { get; set; }
    }
}