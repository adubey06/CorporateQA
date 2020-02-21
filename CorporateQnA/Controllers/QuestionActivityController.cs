using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CorporateQnA.Models;
using CorporateQnA.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CorporateQnA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionActivityController : BaseController
    {
        private IQuestionActivityServices _services;

        public QuestionActivityController(IQuestionActivityServices Services)
        {
            _services = Services;
        }

        [HttpPost]
        public ActionResult<QuestionActivity> Post(QuestionActivity activity)
        {
            var result =  _services.AddActivity(activity);
            if (result == null)
            {
                return new QuestionActivity();
            }
            return result;
        }

        [HttpGet("{userId}/{questionId}")]
        public ActionResult<short> GetStatus(long userId , long questionId)
        {
            return _services.ActivityStatus(userId,questionId);
        }
    }
}