using System;
using System.Collections.Generic;

#nullable disable

namespace Liberty.Models
{
    public partial class Privilege
    {
        public Privilege()
        {
            RolePrivileges = new HashSet<RolePrivilege>();
        }

        public int PrivilegeId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Status { get; set; }

        public virtual ICollection<RolePrivilege> RolePrivileges { get; set; }
    }
}
