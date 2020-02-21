using CorporateQnA.Services.Interfaces;

using System.Collections.Generic;
using Dapper;
using System.Data;
using CorporateQnA.Models;
using System.Linq;
using CorporateQnA.Services.Extensions;

namespace CorporateQnA.Services.Services
{
    public class UserServices: IUserServices
    {
        private IDbConnection _connection;
        public UserServices(IDbConnection connection)
        {
            _connection = connection;
        }

        public long AddNewuser(User user)
        {
            using(IDbConnection Db = _connection)
            {
                string SQLQuery = "INSERT INTO Users(Name,Designation,Role,Location,ProfileImage) VALUES(@Name,@Designation,@Role,@Location,@ProfileImage); SELECT SCOPE_IDENTITY()";
                return Db.ExecuteScalar<long>(SQLQuery, user.MapTo<Data.User>());

            }
        }

        public IEnumerable<UserInfo> GetAllUsers(string searchKey)
        {
            using(IDbConnection Db = _connection)
            {
                string SQLQuery = $"SELECT * from UsersView where lower(Username) like '%{searchKey}%'";
                return Db.Query<Data.UserInfo>(SQLQuery).MapCollectionTo<UserInfo>();
            }         
        }

        public UserInfo GetUser(long Id)
        {
            using (IDbConnection Db = _connection)
            {
                string SQLQuery = "SELECT * FROM UsersView WHERE Id = @id";
                return Db.Query<Data.UserInfo>(SQLQuery, new { id = Id }).FirstOrDefault().MapTo<UserInfo>();
            }
        }

       
    }
}
