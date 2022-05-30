using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Liberty.Models;
using Microsoft.Extensions.Logging;

namespace Liberty.Services
{
    public class LeaveService
    {
        private readonly LIBERTYContext _context;
        private readonly ILogger<LeaveService> _logger;

        public LeaveService(LIBERTYContext context, ILogger<LeaveService> logger)
        {
            _context = context;
            _logger = logger;
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
    }
}