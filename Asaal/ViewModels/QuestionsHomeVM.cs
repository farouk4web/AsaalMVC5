using Asaal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asaal.ViewModels
{
    public class QuestionsHomeVM
    {
        public IEnumerable<Question> Questions { get; set; }

        public IEnumerable<Answer> LastAnswers { get; set; }

        public Society SocietyHero { get; set; }
    }
}