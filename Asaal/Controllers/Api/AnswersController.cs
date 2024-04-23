using Asaal.DTOs;
using Asaal.Models;
using Asaal.Resources;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Asaal.Controllers.Api
{
    [Authorize]
    public class AnswersController : ApiController
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        [HttpPost]
        public IHttpActionResult CreateAnswer(AnswerDto answerDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var answer = new Answer
            {
                Content = answerDto.Content,
                QuestionId = answerDto.QuestionId,
                UserId = User.Identity.GetUserId(),
                PublishDate = DateTime.Now,
            };

            _context.Answers.Add(answer);
            _context.SaveChanges();

            var currentUser = _context.Users.Find(User.Identity.GetUserId());

            answerDto.Id = answer.Id;
            answerDto.QuestionId = answer.QuestionId;
            answerDto.PublishDate = answer.PublishDate;

            answerDto.UserFullName = currentUser.FullName;
            answerDto.UserId = currentUser.Id;
            answerDto.UserProfileImageSrc = currentUser.ProfileImgSrc;


            // send notification

            var questionOwnerId = _context.Questions.Find(answerDto.QuestionId).UserId;
            if (questionOwnerId != currentUser.Id)
            {
                // create notification to question owner
                var notification = new Notification
                {
                    CreatedDate = DateTime.Now,
                    Content = currentUser.FullName + ViewsKeys.notification4newAnswer2questionOwner,
                    QuestionId = answerDto.QuestionId,
                    AnswerId = answerDto.Id,
                    TargetUserId = questionOwnerId
                };
                _context.Notifications.Add(notification);
                _context.SaveChanges();
            }

            return Ok(answerDto);
        }

        [HttpDelete]
        public IHttpActionResult DeleteAnswer(int id)
        {
            var answerInDb = _context.Answers.Find(id);

            if (answerInDb == null)
                return NotFound();

            else if (answerInDb.UserId == User.Identity.GetUserId() || User.IsInRole(RoleName.Admins) || User.IsInRole(RoleName.Supervisors))
            {
                _context.Answers.Remove(answerInDb);
                _context.SaveChanges();

                return Ok();
            }

            return BadRequest();
        }
    }
}
