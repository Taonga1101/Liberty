using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Liberty.Models;
using Microsoft.Extensions.Logging;

namespace Liberty.Services
{
    public class EmployeeService
    {
        
        private readonly LIBERTYContext _context;
            private readonly ILogger<EmployeeService> _logger;

            public EmployeeService(LIBERTYContext context, ILogger<EmployeeService> logger)
            {
                _context = context;
                _logger = logger;
            }

            public dynamic SaveEmployee(EmploymentDetail employmentDetail)
            {
                dynamic response = new ExpandoObject();
                try
                {
                    if (employmentDetail.EmploymentDetailsId == 0)
                    {
                        _context.EmploymentDetails.Add(employmentDetail);

                    }
                    else
                    {
                        _context.EmploymentDetails.Update(employmentDetail);

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
                return _context.EmploymentDetails.ToList();
            }
        }

    
}