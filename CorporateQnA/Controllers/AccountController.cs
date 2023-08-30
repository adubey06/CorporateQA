using CorporateQnA.Models;
using CorporateQnA.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using System;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;

namespace CorporateQnA.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : Controller
    {

        UserManager<AppUser> _userManager;
        SignInManager<AppUser> _signInManager;
        IUserServices _userServices;

        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager, IUserServices userServices)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _userServices = userServices;
        }

        //private async Task<IActionResult> Login(string returnUrl)
        //{
        //    UserLogin model = new UserLogin
        //    {
        //        ReturnUrl = returnUrl,
        //        ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList() //get all external login providers we have written in startup.cs as list 
        //    };
        //    return View(model);
        //}

        [HttpPost("login")]
        public async Task<bool> LoginAsync(UserLogin model, string returnUrl = null)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is not null)
            {
                var result = await _signInManager.PasswordSignInAsync(user.UserName, model.Password, false, false);
                return result.Succeeded;
            }

            return false;
        }
                
        //public IActionResult Register()
        //{
        //    return View();
        //}

        [HttpPost("register")]
        public async Task<bool> Register(UserRegister model, string returnUrl = null)
        {
            var checkUser = await _userManager.FindByEmailAsync(model.Email);
            if(checkUser != null)
            {
                return false;
            }

            User user = new User { Name = model.Name, Designation = model.Designation, Role = model.Role, Location = model.Location, ProfileImage = "assets/images/users.png" };
            long userId = _userServices.AddNewuser(user);
            var appUser = new AppUser { UserName = model.Email, Email = model.Email, UserId = userId };
            var result = await _userManager.CreateAsync(appUser, model.Password);
            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(appUser, isPersistent: false);
                return true;
            }

            return false;
        }

        //public IActionResult AddDetail()
        //{
            
        //    return View();
        //}

        [HttpPost("adddetail")]
        public async Task<IActionResult> AddDetailAsync(UserDetail model, string returnUrl = null)
        {
            string email = TempData["EMAIL"].ToString();
            string name = TempData["NAME"].ToString();
            if (ModelState.IsValid)
            {
                User user = new User { Name = name, Designation = model.Designation, Role = model.Role, Location = model.Location, ProfileImage = "assets/images/users.png" };
                long userId = _userServices.AddNewuser(user);
                var aspNetUser = await _userManager.FindByEmailAsync(email);
                aspNetUser.UserId = userId;
                await _userManager.UpdateAsync(aspNetUser);
                await _signInManager.SignInAsync(aspNetUser, isPersistent: false);
                if (returnUrl == null)
                    return Redirect("~/home");
                return Redirect(returnUrl);
            }
            return View(model);
        }

        //public async Task<IActionResult> Logout()
        //{
        //    await _signInManager.SignOutAsync();
        //    return Redirect("login");
        //}

        [AllowAnonymous]
        [HttpPost("ExternalLogin")]
        public IActionResult ExternalLogin(string provider, string returnUrl)
        {
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account", new { ReturnUrl = returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties); //after returning it will take us to google sign in page and if signin is successfull we go to callback function
        }

        //[AllowAnonymous]
        //public async Task<IActionResult> ExternalLoginCallback( string returnUrl = null, string remoteError = null )
        //{
        //    returnUrl ??= Url.Content("~/");
        //    UserLogin model = new UserLogin
        //    {
        //        ReturnUrl = returnUrl,
        //        ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
        //    };

        //    if(remoteError != null)
        //    {
        //        ModelState.AddModelError(string.Empty, $"Error from external provider : { remoteError }");
        //        return View("Login", model);
        //    }

        //    var info = await _signInManager.GetExternalLoginInfoAsync();
        //    if(info == null)
        //    {
        //        ModelState.AddModelError(string.Empty, $"Error loading external login information");
        //        return View("Login", model);
        //    }

        //        var signInResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: true); //check if user is already registered through third party or not, if preasent sign in
        //    if (signInResult.Succeeded)
        //    {
        //        var user = await _userManager.FindByEmailAsync(info.Principal.FindFirstValue(ClaimTypes.Email));
        //        if (user != null && user.UserId != 0)
        //            return Redirect(returnUrl);

        //        TempData["EMAIL"] = info.Principal.FindFirstValue(ClaimTypes.Email);
        //        TempData["NAME"] = info.Principal.FindFirstValue(ClaimTypes.Name);
        //        return RedirectToAction("AddDetail");
        //    }
        //    else
        //    {
        //        var email = info.Principal.FindFirstValue(ClaimTypes.Email);
        //        if(email != null)
        //        {
        //            var user = await _userManager.FindByEmailAsync(email);

        //            if(user == null)
        //            {

        //                user = new AppUser
        //                {
        //                    UserName = info.Principal.FindFirstValue(ClaimTypes.Email),
        //                     Email = info.Principal.FindFirstValue(ClaimTypes.Email)
        //                };
        //            await _userManager.CreateAsync(user);   
        //            }

        //            await _userManager.AddLoginAsync(user, info); // add login data to asp tables
                    
        //            if (user.UserId!=0)
        //                return Redirect(returnUrl);

        //            TempData["EMAIL"] = info.Principal.FindFirstValue(ClaimTypes.Email);
        //            TempData["NAME"] = info.Principal.FindFirstValue(ClaimTypes.Name);
        //            return Redirect("AddDetail");
        //        }
        //        ViewBag.ErrorTitle = $"Email claim not received from : {info.LoginProvider}";
        //        ViewBag.ErrorMessage = "Please contact Yash Bhatia";
        //        return View("Error");
        //    }
        //}
    }
}