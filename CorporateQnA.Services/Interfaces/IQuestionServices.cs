using CorporateQnA.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CorporateQnA.Services.Interfaces
{
    public interface IQuestionServices
    {
        public IEnumerable<QuestionInfo> GetAllQuestions(long userId, SearchFilters filters);
        public IEnumerable<QuestionInfo> GetMyQuestions(long id);
        public IEnumerable<AnsweredQuestionInfo> GetMyAnsweredQuestions(long id);
        public QuestionInfo GetQuestion(long Id);
        public Question AddQuestion(Question question);
    }
}
