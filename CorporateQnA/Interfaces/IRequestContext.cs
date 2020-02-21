using CorporateQnA.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorporateQnA.Interfaces
{
    public interface IRequestContext
    {
        public Task<CurrentUser> GetUserDetails();
    }
}
