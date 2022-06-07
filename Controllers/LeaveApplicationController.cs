using Liberty.Models;
using Liberty.Services;
using Microsoft.AspNetCore.Mvc;

namespace Liberty.Controllers
{
    public class LeaveApplicationController : Controller
    {
        private readonly LeaveService _leaveService;

        public LeaveApplicationController(LeaveService leaveService)
        {
            _leaveService = leaveService;
            
        }


        // GET
        
        public IActionResult ApplyForLeave()
        {
            var leaveTypes = _leaveService.GetLeaveTypes();
            ViewData["leaveTypes"] = leaveTypes;
            return View();
        }
        public IActionResult LeaveApplications ()
        {
            var leaveApplications = _leaveService.GetLeaveApplications();
            ViewData["leaveApplications"] = leaveApplications;

            return View();
        }
        
        public IActionResult MyLeaveApplication()
        {
            var leaveApplications = _leaveService.GetMyLeaveApplications();
            ViewData["leaveApplications"] = leaveApplications;
            return View("LeaveApplications");
        }
        public IActionResult SupervisorLeaveApplication()
        {
            var leaveApplications = _leaveService.GetPendingLeaveApplications();
            ViewData["leaveApplications"] = leaveApplications;
            ViewData["isSupervisor"] = true;
            return View("LeaveApplications");
        }
        
        [HttpPost]
        public IActionResult SaveLeaveApplication(LeaveApplication leaveApplication)
        {
            return Json(_leaveService.SaveLeaveApplications(leaveApplication));
        }
        
        [HttpPost]
        public IActionResult ApproveApplication(int id)
        {
            return Json(_leaveService.ApproveApplication(id));
        }
        
        [HttpPost]
        public IActionResult RejectApplication(int id)
        {
            return Json(_leaveService.RejectApplication(id));
        }
        
    }
}