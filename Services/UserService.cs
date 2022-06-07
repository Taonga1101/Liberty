using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Security.Claims;
using Liberty.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Liberty.Services
{
    public class UserService
    {
        private readonly LIBERTYContext _context;
        private readonly ILogger<UserService> _logger;
        private readonly ISession _session;
        private readonly string _userId;

        public UserService(IHttpContextAccessor httpContextAccessor, LIBERTYContext context, IConfiguration appConfig, ISession session, ILogger<UserService> logger)
        {
            _context = context;
            _logger = logger;

            HttpContext httpContext = httpContextAccessor.HttpContext;
            if (httpContext != null)
            {
                if (httpContextAccessor.HttpContext != null)
                {
                    _session = httpContextAccessor.HttpContext.Session;
                    _userId = httpContextAccessor.HttpContext.User.Claims
                        .FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;
                }
            }

            _session = session;
        }  

        public User GetCurrentUser()
        {
            var userId = _session.GetInt32("UserId");
            return _context.Users.FirstOrDefault(u => u.UserId == userId);
        }

        public dynamic SaveRole(Role role)
        {
            dynamic response = new ExpandoObject();
            try
            {
                if (role.RoleId == 0)
                {
                    _context.Roles.Add(role);
                }
                else
                {
                    _context.Roles.Update(role);
                }

                _context.SaveChanges();
                response.success = true;
                response.message = "saves";
            }
            catch (Exception e)
            {
                _logger.LogWarning(e.StackTrace);
                response.succss = false;
                response.message = "Failed";
            }

            return response;

        }
        public List<Role> GetRoles()
        {
            return _context.Roles.ToList();
        }

        public dynamic SavePrivileges(Privilege privilege)
        {
            dynamic response =new ExpandoObject();
            try
            {
                if (privilege.PrivilegeId == 0)
                {
                    _context.Privileges.Add(privilege);
                }
                else
                {
                    _context.Privileges.Update(privilege);
                }

                _context.SaveChanges();
                response.success = true;
                response.message = "save";


            }
            catch (Exception e)
            {
                _logger.LogWarning(e.StackTrace);
                response.success = false;
                response.message = "failed";
            }

            return response;
        }

        public List<Privilege> GetPrivileges()
        {
            return _context.Privileges.ToList();
        }
        
        public int SaveUser(User user)
        {
            int userId = 0;
            try
            {
                if (user.UserId == 0)
                {
                    _context.Users.Add(user);
                }
                else
                {
                    _context.Users.Update(user);
                }

                _context.SaveChanges();
                userId = user.UserId;

            }
            catch (Exception e)
            {
                _logger.LogWarning(e.StackTrace);
            }

            return userId;

        }

        public EmploymentDetail GetEmployeeDetails(int userId)
        {
            return _context.EmploymentDetails.Include(p => p.Position)
                .FirstOrDefault(u => u.UserId == userId);
        }
    }
} 