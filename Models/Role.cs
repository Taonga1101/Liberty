using System;
using System.Collections.Generic;

#nullable disable

namespace Liberty.Models
{
    public partial class Role
    {
        public Role()
        {
            RolePrivileges = new HashSet<RolePrivilege>();
            UserRoles = new HashSet<UserRole>();
        }

        public int RoleId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }
        public int SupervisorId { get; set; }

        public virtual ICollection<RolePrivilege> RolePrivileges { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }
    }
}
