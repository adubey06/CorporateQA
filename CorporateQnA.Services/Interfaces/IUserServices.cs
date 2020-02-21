using CorporateQnA.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace CorporateQnA.Services.Interfaces
{
    public interface IUserServices
    {
        IEnumerable<UserInfo> GetAllUsers(string searchKey);
        UserInfo GetUser(long Id);
        long AddNewuser(User user);
    }
}
