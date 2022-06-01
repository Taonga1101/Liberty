using Liberty.DataModels;
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
        
        //employment details
        public IActionResult ManageEmploymentDetails()
        {
            var positions = _employeeService.GetPositions();
            var departments = _employeeService.GetDepartments();
            
            ViewData["positions"] = positions;
            ViewData["departments"] = departments;
            return View();
        }
        public IActionResult EmploymentDetails()
        {
            var employmentDetails = _employeeService.GetEmploymentDetails();
            ViewData["employmentDetails"] = employmentDetails;
            return View();
        }
        
        [HttpPost]
        public IActionResult SaveEmployee(EmployeeDto employmentDetail)
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
        public IActionResult SavePosition(Position position)
        {
            return Json(_employeeService.SavePosition(position));
        }
        
        //department details
        
        public IActionResult ManageDepartments()
        {
            ViewData["offices"] = _employeeService.GetOffices();
            return View();
        }
        public IActionResult Departments()
        {
            var departments = _employeeService.GetDepartments();
            ViewData["departments"] = departments;
            return View();
        }
        
        [HttpPost]
        public IActionResult SaveDepartment(Department department)
        {
            return Json(_employeeService.SaveDepartments(department));
        }
        
       
        
    }

   
}