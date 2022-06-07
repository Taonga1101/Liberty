using System.Linq;
using Liberty.Models;
using Microsoft.EntityFrameworkCore;

namespace Liberty.Services
{
    public class AuthenticationService
    {
        private readonly LIBERTYContext _context;

        public AuthenticationService(LIBERTYContext context)
        {
            _context = context;
        }

        public bool CanLogin(string username, string password)
        {
            var user = _context.UserCredentials.FirstOrDefault(u => u.UserName == username);
            if (user == null)
            {
                return false;
            }
            if (string.IsNullOrEmpty(password))
            {
                return false;
            }

            return password == user.Password;
        }

        public User GetUserProfile(string userName)
        {
            var credential = _context
                .UserCredentials
                .FirstOrDefault(u => u.UserName == userName);
            if (credential == null)
            {
                return null;
            }

            User user = _context
                .Users
                .FirstOrDefault(u => u.UserId == credential.UserId);
            return user;
        }
    }
}