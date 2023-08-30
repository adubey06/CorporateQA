using System.Threading.Tasks;
using CorporateQnA.Interfaces;
using CorporateQnA.Models;
using CorporateQnA.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace CorporateQnA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginInfoController : BaseController
    {
 
        IUserServices _userServices;
        private IRequestContext _requestContext;

        public LoginInfoController(IUserServices userServices,IRequestContext requestContext)
        {
            _userServices = userServices;
            _requestContext = requestContext;
        }

        [HttpGet]
        public async Task<ActionResult<UserInfo>> GetLoggedUserDetailAsync()
        {
            var user = await _requestContext.GetUserDetails();
            UserInfo userInfo = _userServices.GetUser(user.Id);
            return userInfo;
        }
    }
}