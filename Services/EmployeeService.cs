using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Liberty.DataModels;
using Liberty.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace Liberty.Services
{
    public class EmployeeService
    {
        private readonly LIBERTYContext _context;
        private readonly ILogger<EmployeeService> _logger;
        private readonly UserService _userService;

        public EmployeeService(LIBERTYContext context, ILogger<EmployeeService> logger, UserService userService)
        {
            _context = context;
            _logger = logger;
            _userService = userService;
        }
        
        public dynamic SaveEmployee(EmployeeDto employmentDetail)
        {
            dynamic response = new ExpandoObject();
            try
            {
                EmploymentDetail details = new EmploymentDetail();
                details.EmploymentDetailsId = employmentDetail.EmploymentDetailsId;
                details.EmploymentNumber = employmentDetail.EmployeeNumber;
                details.PositionId = employmentDetail.PositionId;
                details.DepartmentId = employmentDetail.DepartmentId;
                details.ContractStart = employmentDetail.ContractStart;
                details.ContractEnd = employmentDetail.ContractEnd;
                details.IsHod = employmentDetail.IsHod;

                User userInfo = new User();
                userInfo.UserId = employmentDetail.UserId;
                userInfo.FirstName = employmentDetail.FirstName;
                userInfo.LastName = employmentDetail.LastName;
                userInfo.MobileNumber = employmentDetail.MobileNumber;
                userInfo.Gender = employmentDetail.Gender;
                userInfo.Email = employmentDetail.Email;
                userInfo.Address = employmentDetail.Address;

             
                details.UserId = _userService.SaveUser(userInfo);
                

                if (details.UserId == 0)
                {
                    throw new Exception("User save error");
                }
             
                if (employmentDetail.EmploymentDetailsId == 0)
                {
                    _context.EmploymentDetails.Add(details);

                }
                else
                {
                    _context.EmploymentDetails.Update(details);

                }

                _context.SaveChanges();
                response.success = true;
                response.message = "Saved";     
                
            }
            catch (Exception e)
            {
                _logger.LogWarning(e.StackTrace);
                response.success = false;
                response.message = "Failed";
            }

            return response;
        }

        public List<EmploymentDetail> GetEmploymentDetails()
        {
            return _context.EmploymentDetails
                .Include("User")
                .Include("Department")
                .Include("Position")
                .ToList();
        }

        public dynamic SavePosition(Position position)
        {
            dynamic response = new ExpandoObject();
            try
            {
                if (position.PositionId == 0)
                {
                    _context.Positions.Add(position);

                }
                else
                {
                    _context.Positions.Update(position);

                }

                _context.SaveChanges();
                response.success = true;
                response.message = "Saved";


            }
            catch (Exception e)
            {
                _logger.LogWarning(e.StackTrace);
                response.success = false;
                response.message = "Failed";
            }

            return response;
        }

        public List<Position> GetPositions()
        {
            return _context.Positions.ToList();
        }


        public dynamic SaveDepartments(Department department)
        {
            dynamic response = new ExpandoObject();
            try
            {
                if (department.DepartmentId == 0)
                {
                    _context.Departments.Add(department);

                }
                else
                {
                    _context.Departments.Update(department);

                }

                _context.SaveChanges();
                response.success = true;
                response.message = "Saved";


            }
            catch (Exception e)
            {
                _logger.LogWarning(e.StackTrace);
                response.success = false;
                response.message = "Failed";
            }

            return response;
        }

        public List<Department> GetDepartments()
        {
            return _context.Departments.Include("Office").ToList();
        }
        
        public List<Office> GetOffices()
        {
            return _context.Offices.ToList();
        }

    }
}
