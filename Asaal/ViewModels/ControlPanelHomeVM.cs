using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Asaal.ViewModels
{
    public class ControlPanelHomeVM
    {
        public int NumberOfAdmins { get; set; }

        public int NumberOfSupervisiors { get; set; }

        public int NumberOfUsers { get; set; }


        public int NumberOfSocieties { get; set; }

        public int NumberOfQuestions { get; set; }

        public int NumberOfNotifications { get; set; }


        public int NumberOfAnswers { get; set; }

        public int NumberOfReplays { get; set; }

        public int NumberOfAnswersWithReplays { get; set; }
    }
}