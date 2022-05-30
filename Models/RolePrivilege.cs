using System;
using System.Collections.Generic;

#nullable disable

namespace Liberty.Models
{
    public partial class RolePrivilege
    {
        public int RolePrivilegeId { get; set; }
        public int RoleId { get; set; }
        public int PrivilegeId { get; set; }

        public virtual Privilege Privilege { get; set; }
        public virtual Role Role { get; set; }
    }
}
