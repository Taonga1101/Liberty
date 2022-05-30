using System;
using System.Collections.Generic;

#nullable disable

namespace Liberty.Models
{
    public partial class LeaveApplication
    {
        public int LeaveApplicationId { get; set; }
        public string Reference { get; set; }
        public int UserId { get; set; }
        public int LeaveTypeId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Reason { get; set; }
        public string EmergencyContact { get; set; }
        public string Attachment { get; set; }
        public bool IsApproved { get; set; }
        public int? ApprovedBy { get; set; }
        public string Status { get; set; }

        public virtual User ApprovedByNavigation { get; set; }
        public virtual LeaveType LeaveType { get; set; }
        public virtual User User { get; set; }
    }
}
