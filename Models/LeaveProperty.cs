using System;
using System.Collections.Generic;

#nullable disable

namespace Liberty.Models
{
    public partial class LeaveProperty
    {
        public int LeavePropertyId { get; set; }
        public int LeaveTypeId { get; set; }
        public string IsComputedBit { get; set; }
        public int MaximumNumberOfDays { get; set; }

        public virtual LeaveType LeaveType { get; set; }
    }
}
