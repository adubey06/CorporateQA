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
    public class QuestionActivityServices : IQuestionActivityServices
    {
        private IDbConnection _connection;

        public QuestionActivityServices(IDbConnection connection)
        {
            _connection = connection;
        }
        public short ActivityStatus(long userId, long questionId)
        {
            using (IDbConnection Db = _connection)
            {
                string sQuery = "Select Type from QuestionActivity where ActivityPerformedOn = @questionId and ActivityBy = @userId and (Type=1 or Type =2)";
                return Db.Query<short>(sQuery, new { userId, questionId }).FirstOrDefault();
            }
        }

        public QuestionActivity AddActivity(QuestionActivity questionActivity)
        {
            using (IDbConnection Db = _connection)
            {
                string sQuery = " from QuestionActivity where ActivityPerformedOn=@AP and ActivityBy=@AB and";
                if (questionActivity.Type == Activity.Like && Db.ExecuteScalar<long>("Select count(id)" + sQuery + " Type =1", new { AP = questionActivity.ActivityPerformedOn, AB = questionActivity.ActivityBy }) > 0)
                {
                    Db.Execute("delete from QuestionActivity where ActivityPerformedOn=@AP and ActivityBy=@AB and Type=1", new { AP = questionActivity.ActivityPerformedOn, AB = questionActivity.ActivityBy });
                    return null;
                }
                else if (questionActivity.Type == Activity.Dislike && Db.ExecuteScalar<long>("Select count(id)" + sQuery + " Type =2", new { AP = questionActivity.ActivityPerformedOn, AB = questionActivity.ActivityBy }) > 0)
                {
                    Db.Execute("delete from QuestionActivity where ActivityPerformedOn=@AP and ActivityBy=@AB and Type=2", new { AP = questionActivity.ActivityPerformedOn, AB = questionActivity.ActivityBy });
                    return null;
                }
                else
                {

                    if (questionActivity.Type == Activity.Like || questionActivity.Type == Activity.Dislike)
                    {
                        Db.Execute("delete from QuestionActivity where ActivityPerformedOn=@AP and ActivityBy=@AB and (Type=1 or Type=2)", new { AP = questionActivity.ActivityPerformedOn, AB = questionActivity.ActivityBy });
                    }

                    string sqlQuery = "Insert into QuestionActivity ( ActivityPerformedOn, ActivityBy , Type,ActivityOn) values(@ActivityPerformedOn, @ActivityBy,@Type,@ActivityOn)";
                    Db.Execute(sqlQuery, questionActivity.MapTo<Data.QuestionActivity>());
                    return Db.Query<Data.QuestionActivity>("Select * from QuestionActivity where ActivityPerformedOn = @AP and Type = @Type", new { AP = questionActivity.ActivityPerformedOn, Type = questionActivity.MapTo<Data.QuestionActivity>().Type }).FirstOrDefault().MapTo<QuestionActivity>();
                }
            }
        }

        public IEnumerable<QuestionActivity> GetAllActivity(long id)
        {
            using (IDbConnection Db = _connection)
            {
                string sQuery = "Select * from QuestionActivity where ActivityBy = @userId";
                return Db.Query<Data.QuestionActivity>(sQuery, new { userId = id }).MapCollectionTo<QuestionActivity>();
            }
        }
    }
}
