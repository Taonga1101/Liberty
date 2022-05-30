using Liberty.Models;
using Liberty.Services;
using Microsoft.AspNetCore.Mvc;

namespace Liberty.Controllers
{
    public class ConfigController : Controller
    {
        private readonly LeaveService _leaveService;

        public ConfigController(LeaveService leaveService)
        {
            _leaveService = leaveService;
        }

        // GET
        public IActionResult Index()
        {

            return View();
        }

        public IActionResult ManageLeaveType()
        {
            return View();
        }

        public IActionResult LeaveTypes()
        {
            var leaveTypes = _leaveService.GetLeaveTypes();
            ViewData["leaveTypes"] = leaveTypes;
            return View();
        }

        [HttpPost]
        public IActionResult SaveLeaveType(LeaveType leaveType)
        {
            return Json(_leaveService.SaveLeaveType(leaveType));
        }
    }

   
}