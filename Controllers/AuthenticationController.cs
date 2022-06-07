using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Liberty.DataModels;
using Liberty.Models;
using Liberty.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using AuthenticationService = Liberty.Services.AuthenticationService;

namespace Liberty.Controllers
{
    public class AuthenticationController : Controller
    {
        private readonly AuthenticationService _authenticationService;
        private readonly ILogger<AuthenticationController> _logger;
        private readonly UserService _userService;

        public AuthenticationController(AuthenticationService authenticationService, ILogger<AuthenticationController> logger, UserService userService)
        {
            _authenticationService = authenticationService;
            _logger = logger;
            _userService = userService;
        }

        // GET
        [HttpPost]
        [Route("authentication/login", Name="Login")]
        public async Task<IActionResult> Login(LoginDto userDetails)
        {
            try
            {
                if (userDetails.UserName.Length < 1 || userDetails.Password.Length < 1)
                {
                    ViewData["error"] = "Email or Password is Incorrect";
                    return View("~/Views/Home/Index.cshtml");
                }

                var canLogin = _authenticationService.CanLogin(userDetails.UserName, userDetails.Password);

                if (!canLogin)
                {
                    ViewData["error"] = "Email or Password is Incorrect";
                    return View("~/Views/Home/Index.cshtml");
                }

                User profileDetails = _authenticationService.GetUserProfile(userDetails.UserName);
                if (profileDetails == null)
                {
                    ViewData["error"] = "Email or Password is Incorrect";
                    return View("~/Views/Home/Index.cshtml");
                }


                HttpContext.Session.SetString("UserName", userDetails.UserName);
                HttpContext.Session.SetInt32("UserId", profileDetails.UserId);
                HttpContext.Session.SetString("Name", profileDetails.FirstName + " " + profileDetails.LastName);
                // HttpContext.Session.SetObjectAsJson();

                var employmentDetails = _userService.GetEmployeeDetails(profileDetails.UserId);
                if (employmentDetails != null)
                {
                    if (employmentDetails.Position != null)
                    {
                        HttpContext.Session.SetString("Position", employmentDetails.Position.Name);
                    }
                    else
                    {
                        HttpContext.Session.SetString("Position", "None");
                    }
                }
                else
                {
                    HttpContext.Session.SetString("Position", "None");
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, userDetails.UserName),
                    new Claim(ClaimTypes.NameIdentifier, profileDetails.UserId.ToString()),
                    new Claim("FullName", profileDetails.FirstName + " " + profileDetails.LastName)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(claimsIdentity);

                var props = new AuthenticationProperties
                {
                    AllowRefresh = true,
                    IsPersistent = true
                };

                HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props).Wait();
                return RedirectToAction("MyLeaveApplication", "LeaveApplication");

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, e.StackTrace);
                ViewData["error"] = "Email or Password is Incorrect";
                return View("~/Views/Home/Index.cshtml");
            }
        }

        public async Task<ActionResult> SignOut()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}