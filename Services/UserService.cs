using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using Liberty.Models;
using Microsoft.Extensions.Logging;

namespace Liberty.Services
{
    public class UserService
    {
        private readonly LIBERTYContext _context;
        private readonly ILogger<UserService> _logger;

        public UserService(LIBERTYContext context, ILogger<UserService> logger)
        {
            _context = context;
            _logger = logger;
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
    }
} 