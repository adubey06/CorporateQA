using CorporateQnA.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CorporateQnA.Services.Interfaces
{
    public interface IAnswerServices
    {
        Answer AddAnswer(Answer answer);
        IEnumerable<AnswerInfo> GetAllAnswers(long id);
        AnswerInfo GetAnswer(long id);
    }
}
