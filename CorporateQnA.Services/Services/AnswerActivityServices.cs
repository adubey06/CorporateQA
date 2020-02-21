using CorporateQnA.Models;
using CorporateQnA.Services.Extensions;
using CorporateQnA.Services.Interfaces;
using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CorporateQnA.Services.Services
{
    public class AnswerActivityServices : IAnswerActivityServices
    {
        private IDbConnection _connection;

        public AnswerActivityServices(IDbConnection connection)
        {
            _connection = connection;
        }
        public short LikeActivityStatus(long userId, long answerId)
        {
            using (IDbConnection Db = _connection)
            {
                string sQuery = "Select Type from AnswerActivity where ActivityPerformedOn = @answerId and ActivityBy = @userId and (Type=1 or Type =2)";
                return Db.Query<short>(sQuery, new { userId, answerId }).FirstOrDefault();
            }
        }
        
        public AnswerActivity AddLikeActivity(AnswerActivity answerActivity)
        {
            using(IDbConnection Db = _connection)
            {
                string sQuery = " from AnswerActivity where ActivityPerformedOn=@AP and ActivityBy=@AB and";
                if (answerActivity.Type == Activity.Like && Db.ExecuteScalar<long>("Select count(id)" + sQuery + " Type =1", new { AP = answerActivity.ActivityPerformedOn, AB = answerActivity.ActivityBy }) > 0)
                {
                    Db.Execute("delete from AnswerActivity where ActivityPerformedOn=@AP and ActivityBy=@AB and Type=1", new { AP = answerActivity.ActivityPerformedOn, AB = answerActivity.ActivityBy });
                    return null;
                }
                else if (answerActivity.Type == Activity.Dislike && Db.ExecuteScalar<long>("Select count(id)" + sQuery + " Type =2", new { AP = answerActivity.ActivityPerformedOn, AB = answerActivity.ActivityBy }) > 0)
                {
                    Db.Execute("delete from AnswerActivity where ActivityPerformedOn=@AP and ActivityBy=@AB and Type=2", new { AP = answerActivity.ActivityPerformedOn, AB = answerActivity.ActivityBy });
                    return null;
                }
                else
                {
                    if (answerActivity.Type == Activity.Like || answerActivity.Type == Activity.Dislike)
                    {
                        Db.Execute("delete from AnswerActivity where ActivityPerformedOn=@AP and ActivityBy=@AB and (Type=1 or Type=2)", new { AP = answerActivity.ActivityPerformedOn, AB = answerActivity.ActivityBy });
                    }

                    string sqlQuery = "Insert into AnswerActivity ( ActivityPerformedOn, ActivityBy , Type,ActivityOn) values(@ActivityPerformedOn, @ActivityBy,@Type,@ActivityOn)";
                    Db.Execute(sqlQuery, answerActivity.MapTo<Data.AnswerActivity>());
                    return Db.Query<Data.AnswerActivity>("Select * from AnswerActivity where ActivityPerformedOn = @AP and Type =Type", new { AP = answerActivity.ActivityPerformedOn, Type = answerActivity.MapTo<Data.AnswerActivity>().Type }).FirstOrDefault().MapTo<AnswerActivity>();
                }
            }
        }
        public AnswerActivity AddBestAnswerActivity(AnswerActivity answerActivity)
        {
            using (IDbConnection Db = _connection)
            {
               
                if (Db.ExecuteScalar<long>("Select Count(id) from AnswerActivity where ActivityPerformedOn=@AP and Type=4", new { AP = answerActivity.ActivityPerformedOn }) > 0)
                {
                    Db.Execute("delete from AnswerActivity where ActivityPerformedOn=@AP and Type=4", new { AP = answerActivity.ActivityPerformedOn });
                    return null;
                }
                else if(answerActivity.Type == Activity.Best)
                {
                    long activityId = Db.ExecuteScalar<long>("Select Id from AnswerActivity inner join (select Id as AnswerId from Answer where QuestionId = (Select QuestionId from Answer where Id = @AP)) as AllAnswers on AnswerActivity.ActivityPerformedOn = AnswerId where AnswerActivity.Type=4", new { AP = answerActivity.ActivityPerformedOn });
                    Db.Execute("delete from AnswerActivity where Id=@activityId", new { activityId });
                }

                string sqlQuery = "Insert into AnswerActivity ( ActivityPerformedOn, ActivityBy , Type, ActivityOn) values (@ActivityPerformedOn, @ActivityBy, @Type, @ActivityOn)";
                Db.Execute(sqlQuery, answerActivity.MapTo<Data.AnswerActivity>());
                return Db.Query<Data.AnswerActivity>("Select * from AnswerActivity where ActivityPerformedOn = @AP and Type = 4", new { AP = answerActivity.ActivityPerformedOn } ).FirstOrDefault().MapTo<AnswerActivity>();
            }
        }

        public IEnumerable<AnswerActivity> GetAllActivity()
        {
            using (IDbConnection Db = _connection)
            {
                string sQuery = "Select * from AnswerActivity";
                return Db.Query<Data.AnswerActivity>(sQuery).MapCollectionTo<AnswerActivity>();
            }
        }

    }
}
