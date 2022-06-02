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