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
    public class AnswerActivityController : BaseController
    {
        private IAnswerActivityServices _services;
       
        public AnswerActivityController(IAnswerActivityServices services)
        {
            _services = services;
        }

        [HttpPost]
        public ActionResult<AnswerActivity> AddActivity(AnswerActivity answerActivity)
        {
            var result = (answerActivity.Type == Activity.Best) ?
                _services.AddBestAnswerActivity(answerActivity) : _services.AddLikeActivity(answerActivity);

            if (result == null)
            {
                return new AnswerActivity();
            }
            return result;
        }
    }
}