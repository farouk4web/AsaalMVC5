using Asaal.Models;
using Asaal.ViewModels;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Asaal.Controllers
{
    public class SocietiesController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        // GET: Categories
        public ActionResult Index()
        {
            var societies = _context.Societies.OrderBy(c => c.Questions.Count()).ToList();
            return View(societies);
        }

        public ActionResult SocietyInDetails(int id)
        {
            var society = _context.Societies.Find(id);

            if (society == null)
                return HttpNotFound();

            var lastAnswersInSociety = _context.Answers.
                    Where(c => c.Question.SocietyId == id).OrderByDescending(m => m.PublishDate).Take(7).ToList();

            var bestWriters = _context.Users
                    .Where(m => m.Questions.Where(q => q.SocietyId == id).Count() != 0)
                        .OrderByDescending(c => c.Questions.Count()).Take(5).ToList();

            var viewModel = new SocietyInDetailsVM
            {
                Society = society,
                BestUsersInSociety = bestWriters,
                LastAnswersInSociety = lastAnswersInSociety
            };

            return View(viewModel);
        }



        /*
            1 ---  this controller is just to get all categories and category with more details
            2 ---  and crud operation is just to admins and supervisors onlay,
            3 ---  and thir is no "Delete" Action for category

                cuse.. if category deleted you should delete every question related with it.
                and comments related with questions and its replays
         */

    }
}