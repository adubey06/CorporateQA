using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CorporateQnA.Models;
using CorporateQnA.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CorporateQnA.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class UserInfoController : BaseController
    {
        private IUserServices _service;
        public UserInfoController(IUserServices service)
        {
            _service = service;
        }

        [HttpGet]
        public IEnumerable<UserInfo> Get(string SearchKey)
        {
            if (SearchKey == null)
                SearchKey = "";
            return _service.GetAllUsers(SearchKey);
        }

        [HttpGet("{id}")]
        public ActionResult<UserInfo> GetUserById(long id)
        {
            UserInfo user = _service.GetUser(id);
            if (user == null)
                return NotFound();
            return user;
        }
    }
}