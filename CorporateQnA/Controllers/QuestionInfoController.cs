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
    public class QuestionInfoController : BaseController
    {
        private IQuestionServices _questionServices;
        private IQuestionActivityServices _questionActivityServices;
        private UserManager<AppUser> _userManager;
        public QuestionInfoController(IQuestionServices questionServices, IQuestionActivityServices questionActivityServices,
            UserManager<AppUser> userManager)
        {
            _questionServices = questionServices;
            _questionActivityServices = questionActivityServices;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IEnumerable<QuestionInfo>> GetAllQuestionsAsync([FromQuery] SearchFilters filters)
        {
            long userId =  (await _userManager.FindByIdAsync(_userManager.GetUserId(HttpContext.User))).UserId;
            var result = _questionServices.GetAllQuestions(userId, filters);
            var activity = _questionActivityServices.GetAllActivity(userId);
            foreach (var question in result)
            {
                question.Status = (from a in activity
                                   where a.ActivityPerformedOn == question.Id && (a.Type == Activity.Like || a.Type == Activity.Dislike)
                                   select (short)a.Type).FirstOrDefault();
                
            }
            return result;
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<QuestionInfo>> GetQuestionAsync(long id)
        {
            long userId = (await _userManager.FindByIdAsync(_userManager.GetUserId(HttpContext.User))).UserId;
            var result = _questionServices.GetQuestion(id);
            if (result == null)
                return NotFound();
            result.Status = _questionActivityServices.ActivityStatus(userId, result.Id);
            return result;
        }

        [HttpGet("MyQuestion/{id}")]
        public async Task<IEnumerable<QuestionInfo>> GetMyQuestionsAsync( long id)
        {
            long userId = (await _userManager.FindByIdAsync(_userManager.GetUserId(HttpContext.User))).UserId;
            var result = _questionServices.GetMyQuestions(id);
            var activity = _questionActivityServices.GetAllActivity(userId);
            foreach (var question in result)
            {
                question.Status = (from a in activity
                                   where a.ActivityPerformedOn == question.Id && (a.Type == Activity.Like || a.Type == Activity.Dislike)
                                   select (short)a.Type).FirstOrDefault();
            }
            return result;
        }

        [HttpGet("MyAnswered/{id}")]
        public async Task<IEnumerable<AnsweredQuestionInfo>> GetMyAnsweredQuestionsAsync(long id)
        {
            long userId = (await _userManager.FindByIdAsync(_userManager.GetUserId(HttpContext.User))).UserId;
            var result = _questionServices.GetMyAnsweredQuestions(id);
            var activity = _questionActivityServices.GetAllActivity(userId);
            foreach (var question in result)
            {
                question.Status = (from a in activity
                                   where a.ActivityPerformedOn == question.Id && (a.Type == Activity.Like || a.Type == Activity.Dislike)
                                   select (short)a.Type).FirstOrDefault();
            }
            return result;
        }

        [HttpPost]
        public Question AddQuestion(Question question)
        {
            return _questionServices.AddQuestion(question);
        }
    } 
}