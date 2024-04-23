using Asaal.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asaal.ViewModels
{
    public class NewQuestionVM
    {
        public Question Question { get; set; }

        public IEnumerable<Society> Societies { get; set; }
    }
}