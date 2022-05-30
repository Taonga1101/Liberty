using Liberty.Models;
using Liberty.Services;
using Microsoft.AspNetCore.Mvc;

namespace Liberty.Controllers
{
    public class ConfigController : Controller
    {
        private readonly LeaveService _leaveService;
        private readonly EmployeeService _employeeService;

        public ConfigController(LeaveService leaveService, EmployeeService employeeService)
        {
            _leaveService = leaveService;
            _employeeService = employeeService;
        }

        // GET
        public IActionResult Index()
        {

            return View();
        }
        //leave details

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
        
        //employment detals
        public IActionResult ManageEmploymentDetails()
        {
            return View();
        }
        public IActionResult EmploymentDetails()
        {
            var employmentDetails = _employeeService.GetEmploymentDetails();
            ViewData["employmentDetails"] = employmentDetails;
            return View();
        }
        
        [HttpPost]
        public IActionResult SaveEmployee(EmploymentDetail employmentDetail)
        {
            return Json(_employeeService.SaveEmployee(employmentDetail));
        }
        
        //position details
        public IActionResult ManagePositions()
        {
            return View();
        }

        public IActionResult Positions()
        {
            var positions = _employeeService.GetPositions();
            ViewData["positions"] = positions;
            return View();
        }
        
        [HttpPost]
        public IActionResult SavePositions(Position position)
        {
            return Json(_employeeService.SavePosition(position));
        }
        
        
    }

   
}