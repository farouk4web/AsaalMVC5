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
    public class ReplaysController : ApiController
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        [HttpPost]
        public IHttpActionResult CreateReplay(ReplayDto replayDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var currentUser = _context.Users.Find(User.Identity.GetUserId());

            var replay = new Replay
            {
                Content = replayDto.Content,
                AnswerId = replayDto.AnswerId,
                PublishDate = DateTime.Now,
                UserId = currentUser.Id
            };

            _context.Replays.Add(replay);
            _context.SaveChanges();
            
            //populate DTO to send data to view
            replayDto.Id = replay.Id;
            replayDto.AnswerId= replay.AnswerId;
            replayDto.PublishDate = replay.PublishDate;
            replayDto.UserFullName = currentUser.FullName;
            replayDto.UserId = currentUser.Id;
            replayDto.UserProfileImageSrc = currentUser.ProfileImgSrc;


            // send notifications
            var answerOwnerId = _context.Answers.Find(replayDto.AnswerId).UserId;
            var questionOwnerId = _context.Questions.Find(replay.Answer.QuestionId).UserId;

            // create notification To answer Owner
            if (answerOwnerId != currentUser.Id)
            {
                var notificationToanswerOwner = new Notification
                {
                    CreatedDate = DateTime.Now,
                    Content = currentUser.FullName + ViewsKeys.notification4newReplay2answerOwner,
                    QuestionId = replay.Answer.QuestionId,
                    AnswerId = replayDto.AnswerId,
                    TargetUserId = answerOwnerId
                };

                _context.Notifications.Add(notificationToanswerOwner);
                _context.SaveChanges();
            }


            // create notification To Question Owner
            if (questionOwnerId != currentUser.Id && questionOwnerId != answerOwnerId)
            {
                var notificationToQuestionOwner = new Notification
                {
                    CreatedDate = DateTime.Now,
                    Content = currentUser.FullName + ViewsKeys.notification4newReplay2questionOwner,
                    QuestionId = replay.Answer.QuestionId,
                    AnswerId = replayDto.AnswerId,
                    TargetUserId = replay.Answer.Question.UserId
                };

                _context.Notifications.Add(notificationToQuestionOwner);
                _context.SaveChanges();
            }

            return Ok(replayDto);
        }

        [HttpDelete]
        public IHttpActionResult DeleteReplay(int id)
        {
            var replayInDb = _context.Replays.Find(id);

            if (replayInDb == null)
                return NotFound();

            else if (replayInDb.UserId == User.Identity.GetUserId() || User.IsInRole(RoleName.Admins) || User.IsInRole(RoleName.Supervisors))
            {
                _context.Replays.Remove(replayInDb);
                _context.SaveChanges();

                return Ok();
            }

            return BadRequest();
        }
    }
}
