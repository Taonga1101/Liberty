using System;
using System.Collections.Generic;

#nullable disable

namespace Liberty.Models
{
    public partial class LeaveType
    {
        public LeaveType()
        {
            LeaveApplications = new HashSet<LeaveApplication>();
            LeaveProperties = new HashSet<LeaveProperty>();
            LeaveSchemes = new HashSet<LeaveScheme>();
        }

        public int LeaveTypeId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public string Status { get; set; }

        public virtual ICollection<LeaveApplication> LeaveApplications { get; set; }
        public virtual ICollection<LeaveProperty> LeaveProperties { get; set; }
        public virtual ICollection<LeaveScheme> LeaveSchemes { get; set; }
    }
}
