using CorporateQnA.Interfaces;
using CorporateQnA.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CorporateQnA.Helpers
{
    public class RequestContext : IRequestContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private UserManager<AppUser> _userManager;

        public RequestContext(IHttpContextAccessor httpContextAccessor, UserManager<AppUser> userManager)
        {
            _httpContextAccessor = httpContextAccessor;
            _userManager = userManager;
        }
       
        public async Task<CurrentUser> GetUserDetails()
        {
            var userDetail = await _userManager.FindByIdAsync(_userManager.GetUserId(_httpContextAccessor.HttpContext.User));
            CurrentUser user = new CurrentUser()
            {
                Id = userDetail.UserId,
                Name = userDetail.UserName,
                Email = userDetail.Email
            };
            return user;
        }
    }
}
