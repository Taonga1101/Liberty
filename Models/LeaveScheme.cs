using System;
using System.Collections.Generic;

#nullable disable

namespace Liberty.Models
{
    public partial class LeaveScheme
    {
        public int SchemeId { get; set; }
        public int LeaveTypeId { get; set; }
        public int NumberOfDays { get; set; }
        public int NumberOfMonths { get; set; }
        public string Description { get; set; }
        public string Name { get; set; }

        public virtual LeaveType LeaveType { get; set; }
    }
}
