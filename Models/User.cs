using System;
using System.Collections.Generic;

#nullable disable

namespace Liberty.Models
{
    public partial class User
    {
        public User()
        {
            EmploymentDetails = new HashSet<EmploymentDetail>();
            LeaveApplicationApprovedByNavigations = new HashSet<LeaveApplication>();
            LeaveApplicationUsers = new HashSet<LeaveApplication>();
            UserCredentials = new HashSet<UserCredential>();
            UserRoles = new HashSet<UserRole>();
        }

        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public virtual ICollection<EmploymentDetail> EmploymentDetails { get; set; }
        public virtual ICollection<LeaveApplication> LeaveApplicationApprovedByNavigations { get; set; }
        public virtual ICollection<LeaveApplication> LeaveApplicationUsers { get; set; }
        public virtual ICollection<UserCredential> UserCredentials { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
