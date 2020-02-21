using CorporateQnA.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using Dapper;
using CorporateQnA.Services.Interfaces;
using CorporateQnA.Services.Extensions;
using System.Linq;

namespace CorporateQnA.Services.Services
{
    public class QuestionServices: IQuestionServices
    {
        private IDbConnection _connection;
        public QuestionServices(IDbConnection connection)
        {
            _connection = connection;
        }

        public Question AddQuestion(Question question)
        {
            using(IDbConnection Db = _connection)
            {
                string sQuery = "INSERT INTO Question (Title,Description,CategoryId,AskedBy,AskedOn) VALUES(@Title,@Description,@CategoryId,@AskedBy,@AskedOn)";
                Db.Execute(sQuery, question.MapTo<Data.Question>());
                return Db.Query<Data.Question>("Select * from Question where Description=@Description", new { Description = question.Description }).FirstOrDefault().MapTo<Question>();
            }   
        }

        public IEnumerable<QuestionInfo> GetAllQuestions(long userId,SearchFilters filters)
        {
            
            using(IDbConnection Db = _connection)
            {
                
                if(filters.SearchKey == null)
                {
                    filters.SearchKey = "";
                }
                string sQuery = $"SELECT * FROM QUESTIONSVIEW WHERE (TITLE LIKE @SEARCHKEY OR DESCRIPTION LIKE @SEARCHKEY)";
                if (filters.CategoryId != 0)
                {
                    sQuery = $"{sQuery} AND CATEGORYID = @CATEGORYID";
                }

                if (filters.Show == ShowOption.MyQuestions)
                {
                    sQuery = $"{sQuery} AND ASKEDBYID = @USERID";
                }
                else if(filters.Show == ShowOption.MyParticipation)
                {
                    sQuery = $"{sQuery} AND (SELECT COUNT(*) FROM ANSWER WHERE QUESTIONID = QUESTIONSVIEW.ID AND ANSWEREDBY = @USERID)>0";
                }

                if (filters.SortBy == SortByOption.Recent)
                {
                    sQuery = $"{sQuery} ORDER BY ASKEDON DESC";
                }
                return Db.Query<Data.QuestionInfo>(sQuery, new { USERID = userId, CATEGORYID = filters.CategoryId, SEARCHKEY = $"%{filters.SearchKey}%" }).MapCollectionTo<QuestionInfo>();
            }
        }

        public IEnumerable<AnsweredQuestionInfo> GetMyAnsweredQuestions(long id)
        {
            using(IDbConnection Db = _connection)
            {
                string sQuery = "SELECT * FROM ParticipationView where AnsweredById = @Id ORDER BY AnsweredOn DESC";
                return Db.Query<Data.AnsweredQuestionInfo>(sQuery, new { Id = id }).MapCollectionTo<AnsweredQuestionInfo>();
            }
        }

        public IEnumerable<QuestionInfo> GetMyQuestions(long id)
        {
            using (IDbConnection Db = _connection)
            {
                string sQuery = "SELECT * FROM QuestionsView where AskedById = @Id ORDER BY AskedOn DESC";
                return Db.Query<Data.QuestionInfo>(sQuery,new {Id = id }).MapCollectionTo<QuestionInfo>();
            }
        }

        public QuestionInfo GetQuestion(long id)
        {
            using(IDbConnection Db = _connection)
            {
                string sQuery = "SELECT * FROM QuestionsView WHERE Id = @Id";
                return Db.Query<Data.QuestionInfo>(sQuery,new { Id = id}).FirstOrDefault().MapTo<QuestionInfo>();
            }
        }
    }
}
