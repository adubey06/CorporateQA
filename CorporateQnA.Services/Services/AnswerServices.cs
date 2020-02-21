using CorporateQnA.Models;
using CorporateQnA.Services.Extensions;
using CorporateQnA.Services.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CorporateQnA.Services.Services
{
    public class AnswerServices : IAnswerServices
    {
        private IDbConnection _connection;
        public AnswerServices(IDbConnection connection)
        {
            _connection = connection;
        }
        public Answer AddAnswer(Answer answer)
        {
            using(IDbConnection Db = _connection)
            {
                string sQuery = "INSERT INTO Answer (QuestionId,AnsweredBy,Description,AnsweredOn) VALUES(@QuestionId,@AnsweredBy,@Description,@AnsweredOn)";
                Db.Execute(sQuery, answer.MapTo<Data.Answer>());
                return Db.Query<Data.Answer>("Select * from Answer where Description = @Description", new { Description = answer.Description }).FirstOrDefault().MapTo<Answer>();
            }
        }

        public IEnumerable<AnswerInfo> GetAllAnswers(long id)
        {
            using(IDbConnection Db = _connection)
            {
                string sQuery = "SELECT * FROM AnswersView where AnsweredFor=@Id";
                return Db.Query<Data.AnswerInfo>(sQuery,new { Id = id }).MapCollectionTo<AnswerInfo>();
            }
        }

        public AnswerInfo GetAnswer(long id)
        {
            using(IDbConnection Db = _connection)
            {
                string sQuery = "SELECT * FROM AnswersView where Id=@Id";
                return Db.Query<Data.AnswerInfo>(sQuery, new { Id = id }).FirstOrDefault().MapTo<AnswerInfo>();
            }
        }
    }
}
