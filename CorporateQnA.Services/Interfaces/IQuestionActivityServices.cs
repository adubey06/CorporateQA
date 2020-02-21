using CorporateQnA.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CorporateQnA.Services.Interfaces
{
    public interface IQuestionActivityServices
    {
        QuestionActivity AddActivity(QuestionActivity questionActivity);
        IEnumerable<QuestionActivity> GetAllActivity(long id);
        short ActivityStatus(long userId, long questionId);
    }
}
