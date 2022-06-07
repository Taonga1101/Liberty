using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Liberty.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
namespace Liberty.Services
{
    public class LeaveService
    {
        private readonly LIBERTYContext _context;
        private readonly ILogger<LeaveService> _logger;
        private readonly UserService _userService;

        public LeaveService(LIBERTYContext context, ILogger<LeaveService> logger, UserService userService)
        {
            _context = context;
            _logger = logger;
            _userService = userService;
        }

        public dynamic SaveLeaveType(LeaveType leaveType)
        {
            dynamic response = new ExpandoObject();
            try
            {
                if (leaveType.LeaveTypeId == 0)
                {
                    _context.LeaveTypes.Add(leaveType);

                }
                else
                {
                    _context.LeaveTypes.Update(leaveType);

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

        public List<LeaveType> GetLeaveTypes()
        {
            return _context.LeaveTypes.ToList();
        }

        private string GenerateReference()
        {
            string r = GetRandom();
            var app = _context.LeaveApplications.FirstOrDefault(x => x.Reference == r);
            
            if (app != null)
            {
                while (app != null)
                {
                    r = GetRandom();
                    app = _context.LeaveApplications.FirstOrDefault(x => x.Reference == r);
                
                }
            }

            return r;
        }
        
        private string GetRandom()
        {
            Random ran = new Random();
            return ran.Next(10000000).ToString();
        }
        
        public dynamic SaveLeaveApplications(LeaveApplication leaveApplication)
        {
            dynamic response = new ExpandoObject();
            try
            {
                leaveApplication.UserId = _userService.GetCurrentUser().UserId;
                leaveApplication.Status = "Pending";
                leaveApplication.Reference = GenerateReference();
                if (leaveApplication.LeaveApplicationId == 0)
                {
                    _context.LeaveApplications.Add(leaveApplication);
                }
                else
                {
                    _context.LeaveApplications.Update(leaveApplication);
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
        
        public dynamic ApproveApplication(int id)
        {
            dynamic response = new ExpandoObject();
            try
            {
                var leaveApplication = _context.LeaveApplications.FirstOrDefault(i => i.LeaveApplicationId == id);

                if (leaveApplication == null)
                {
                    response.success = false;
                    response.message = "Unknown ID";
                    return response;
                }
                
                leaveApplication.ApprovedBy = _userService.GetCurrentUser().UserId;
                leaveApplication.Status = "Approved";
                leaveApplication.IsApproved = true;
     
                    _context.LeaveApplications.Update(leaveApplication);
                
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
        
        public dynamic RejectApplication(int id)
        {
            dynamic response = new ExpandoObject();
            try
            {
                var leaveApplication = _context.LeaveApplications.FirstOrDefault(i => i.LeaveApplicationId == id);

                if (leaveApplication == null)
                {
                    response.success = false;
                    response.message = "Unknown ID";
                    return response;
                }
                
                leaveApplication.ApprovedBy = _userService.GetCurrentUser().UserId;
                leaveApplication.Status = "Rejected";
                leaveApplication.IsApproved = true;
     
                _context.LeaveApplications.Update(leaveApplication);
                
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

        public List<LeaveApplication> GetLeaveApplications()
        {
            return _context
                .LeaveApplications
                .Include("User")
                .Include("LeaveType")
                .ToList();
        }
        public List<LeaveApplication> GetPendingLeaveApplications()
        {
            return _context
                .LeaveApplications
                .Include("User")
                .Include("LeaveType")
                .Where(l => !l.IsApproved)
                .ToList();
        }
        
        public List<LeaveApplication> GetMyLeaveApplications()
        {
            int userId = _userService.GetCurrentUser().UserId;
            return _context
                .LeaveApplications
                .Include("User")
                .Include("LeaveType")
                .Where(l => l.UserId == userId)
                .ToList();
        }
    }
    
    
}