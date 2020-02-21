using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CorporateQnA.Models;
using CorporateQnA.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace CorporateQnA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AnswerInfoController : BaseController
    {
        private IAnswerServices _answerServices;
        private IAnswerActivityServices _answerActivityServices;
        private UserManager<AppUser> _userManager;
        public AnswerInfoController(IAnswerServices answerServices,IAnswerActivityServices answerActivityServices,UserManager<AppUser> userManager)
        {
            _answerServices = answerServices;
            _answerActivityServices = answerActivityServices;
            _userManager = userManager;
        }

        [HttpGet("{id}")]
        public async Task<IEnumerable<AnswerInfo>> GetAllAnswersAsync(long id)
        {
            var result = _answerServices.GetAllAnswers(id);
            long userId = (await _userManager.FindByIdAsync(_userManager.GetUserId(HttpContext.User))).UserId;
            var activity = _answerActivityServices.GetAllActivity();
            foreach(var answer in result)
            {
                answer.Status = (from a in activity
                                 where a.ActivityPerformedOn == answer.Id && a.ActivityBy == userId && (a.Type == Activity.Like || a.Type == Activity.Dislike)
                                 select (short)a.Type).FirstOrDefault();

                answer.Best = ((from a in activity
                               where a.ActivityPerformedOn == answer.Id && a.Type == Activity.Best
                               select (short)a.Type).FirstOrDefault() == 4 ) ? true : false;
            }
            return result;
        }

        [HttpGet("Single/{id}")]
        public async Task<ActionResult<AnswerInfo>> GetAnswerAsync(long id)
        {
            var result = _answerServices.GetAnswer(id);
            long userId = (await _userManager.FindByIdAsync(_userManager.GetUserId(HttpContext.User))).UserId;
            if (result == null)
            {
                return NotFound();
            }
            result.Status = _answerActivityServices.LikeActivityStatus(userId, result.Id); 
            return result;
        }

        [HttpPost]
        public ActionResult<Answer> AddAnswer(Answer answer)
        {
            return _answerServices.AddAnswer(answer);
        }
    }
}