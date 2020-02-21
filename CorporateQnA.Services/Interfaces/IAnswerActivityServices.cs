using CorporateQnA.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CorporateQnA.Services.Interfaces
{
    public interface IAnswerActivityServices
    {
        AnswerActivity AddLikeActivity(AnswerActivity answerActivity);
        AnswerActivity AddBestAnswerActivity(AnswerActivity answerActivity);
        IEnumerable<AnswerActivity> GetAllActivity();
        short LikeActivityStatus(long userId, long answerId);
    }
}
