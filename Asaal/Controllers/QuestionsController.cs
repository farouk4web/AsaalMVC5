using Asaal.DTOs;
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
    [Authorize]
    public class QuestionsController : Controller
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        // GET: Questions
        [AllowAnonymous]
        public ActionResult Index(int? pageN, int? elementsPerPage)
        {
            var pageNumber = 1;
            var elementInPage = 8;

            if (pageN.HasValue)
                pageNumber = (int)pageN.Value;

            if (elementsPerPage.HasValue)
                elementInPage = (int)elementsPerPage.Value;

            var startIndex = (pageNumber - 1) * elementInPage;
            var allQuestions = _context.Questions
                                            .OrderByDescending(q => q.PublishDate)
                                                .Skip(startIndex)
                                                    .Take(elementInPage)
                                                        .ToList();

            // send page number to create pagenation nev elements
            ViewBag.currentPageNumber = pageNumber;
            ViewBag.countOfQuestions = _context.Questions.ToList().Count();

            var lastAnswers = _context.Answers.OrderByDescending(m => m.PublishDate).Take(7).ToList();
            var societis = _context.Societies.OrderByDescending(m => m.Questions.Count()).Take(1).ToList();
            var societyHero = societis.FirstOrDefault();

            var viewModel = new QuestionsHomeVM
            {
                Questions = allQuestions,
                LastAnswers= lastAnswers,
                SocietyHero = societyHero
            };
            return View(viewModel);
        }

        [AllowAnonymous]
        public ActionResult QuestionInDetails(int id)
        {
            var question = _context.Questions.Find(id);

            if (question == null)
                return HttpNotFound();

            //count views
            question.Views = question.Views + 1;
            _context.SaveChanges();

            var highViewQuestions = _context.Questions.OrderByDescending(m => m.Views).Take(5).ToList();

            var viewModel = new QuestionInDetailsVM
            {
                Question = question,
                AnswerDto = new AnswerDto(),
                HighViewQuestion = highViewQuestions
            };

            return View(viewModel);
        }

        public ActionResult New()
        {
            var viewModel = new NewQuestionVM
            {
                Question = new Question(),
                Societies = _context.Societies.ToList()
            };
            return View("QuestionForm", viewModel);
        }

        public ActionResult Update(int id)
        {
            var questionInDb = _context.Questions.Find(id);
            if (questionInDb == null)
                return HttpNotFound();

            // if he is the owner or admin or supervisor
            else if (questionInDb.UserId == User.Identity.GetUserId() || User.IsInRole(RoleName.Admins) || User.IsInRole(RoleName.Supervisors))
            {
                var viewModel = new NewQuestionVM
                {
                    Question = questionInDb,
                    Societies = _context.Societies.ToList()
                };

                return View("QuestionForm", viewModel);
            }

            return HttpNotFound();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Save(NewQuestionVM viewModel)
        {
            if (!ModelState.IsValid)
            {
                var vm = new NewQuestionVM
                {
                    Question = viewModel.Question,
                    Societies = _context.Societies.ToList()
                };
                return View("QuestionForm", vm);
            }

            if (viewModel.Question.Id == 0)
            {
                //New Question
                viewModel.Question.UserId = User.Identity.GetUserId();
                viewModel.Question.PublishDate = DateTime.Now;
                viewModel.Question.Views= 0;

                _context.Questions.Add(viewModel.Question);
            }
            else
            {
                //ancient Category
                var questionInDb = _context.Questions.Find(viewModel.Question.Id);
                if (questionInDb == null)
                    return HttpNotFound();

                else if (questionInDb.UserId == User.Identity.GetUserId() || User.IsInRole(RoleName.Admins) || User.IsInRole(RoleName.Supervisors))
                {
                    questionInDb.Title = viewModel.Question.Title;
                    questionInDb.Content= viewModel.Question.Content;
                    questionInDb.SocietyId= viewModel.Question.SocietyId;
                }
            }

            _context.SaveChanges();

            return RedirectToAction("QuestionInDetails", new { id = viewModel.Question.Id });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var questionInDb = _context.Questions.Find(id);
            if (questionInDb == null)
                return HttpNotFound();

            else if (questionInDb.UserId == User.Identity.GetUserId() || User.IsInRole(RoleName.Admins) || User.IsInRole(RoleName.Supervisors))
            {
                _context.Questions.Remove(questionInDb);
                _context.SaveChanges();
            }
           

            return RedirectToAction("Index");
        }
    }
}