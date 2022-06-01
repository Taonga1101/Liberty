using Liberty.Models;
using Liberty.Services;
using Microsoft.AspNetCore.Mvc;

namespace Liberty.Controllers
{
    public class UserController : Controller
    {
        private readonly UserService _userService;

        public UserController(UserService userService)
        {
            _userService = userService;
        }

        // GET
        public IActionResult Index()
        {
            return View();
        }


        public IActionResult ManageRole()
        {
            return View();
        }
        
        public IActionResult Role()
        {
            var role = _userService.GetRoles();
            ViewData["role"] = role;
            return View();
        }
        [HttpPost]
        public IActionResult SaveRole(Role role)
        {
            return Json(_userService.SaveRole(role));
        }
        
        public IActionResult ManagePivilege()
        {
            return View();
        }

        public IActionResult Privilege()
        {
            var privileges = _userService.GetPrivileges();
            ViewData["privileges"] = privileges;
            return View();
        }
        
        [HttpPost]
        public IActionResult SavePrivileges(Privilege privilege)
        {
            return Json(_userService.SavePrivileges(privilege));
        }
    }
}