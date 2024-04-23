using Asaal.DTOs;
using Asaal.Models;
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
    public class VotesController : ApiController
    {
        private ApplicationDbContext _context = new ApplicationDbContext();

        [HttpPost]
        public IHttpActionResult CreateNewVote(VoteDto voteDto)
        {
            if (!ModelState.IsValid)
                return BadRequest();

            var currentUserId = User.Identity.GetUserId();

            // user trying to vote his answer
            var answerInDb = _context.Answers.Find(voteDto.AnswerId);
            var voteInDb = _context.Votes.SingleOrDefault(m => m.AnswerId == voteDto.AnswerId && m.UserId == currentUserId);

            if (answerInDb != null && answerInDb.UserId == currentUserId)
                return Json("ownTheAnswer");

            // user vote before on this answer so he cant vote again
            else if (voteInDb != null)
                return Json("userVoteBefore");

            // if this first time add his vote in this answer he can vote
            else
            {
                var vote = new Vote
                {
                    AnswerId = voteDto.AnswerId,
                    IsGoodAnswer = voteDto.IsGoodAnswerOrNot,
                    UserId = currentUserId
                };

                _context.Votes.Add(vote);
                _context.SaveChanges();

                return Ok();
            }
        }

        //[HttpDelete]
        //public IHttpActionResult RemoveVote(VoteDto voteDto)
        //{
        //    if (!ModelState.IsValid)
        //        return BadRequest();

        //    // if user voted before return bad request
        //    var currentUserId = User.Identity.GetUserId();

        //    var voteInDb = _context.Votes.SingleOrDefault(m => m.AnswerId == voteDto.AnswerId && m.UserId == currentUserId);
        //    if (voteInDb != null)
        //        return BadRequest();

        //    // if this first time add his vote in this answer he can vote
        //    var vote = new Vote
        //    {
        //        AnswerId = voteDto.AnswerId,
        //        IsGoodAnswer = voteDto.IsGoodAnswerOrNot,
        //        UserId = currentUserId
        //    };

        //    _context.Votes.Add(vote);
        //    _context.SaveChanges();

        //    return Ok(vote);
        //}

    }
}
