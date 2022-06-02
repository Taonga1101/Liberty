using Liberty.Models;
using Microsoft.AspNetCore.Mvc;

namespace Liberty.Controllers
{
    public class LeaveApplicationController : Controller
    {
        private readonly LeaveApplication _leaveService;

        public LeaveApplicationController(LeaveApplication leaveService)
        {
            _leaveService = leaveService;
            
        }


        // GET
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult ApplyForLeave()
        {
            return View();
        }
        public IActionResult LeaveApplications ()
        {

            return View();
        }
        
        


    }
}