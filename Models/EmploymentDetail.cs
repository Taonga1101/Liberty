using System;
using System.Collections.Generic;

#nullable disable

namespace Liberty.Models
{
    public partial class EmploymentDetail
    {
        public int EmploymentDetailsId { get; set; }
        public string EmploymentNumber { get; set; }
        public int PositionId { get; set; }
        public int DepartmentId { get; set; }
        public DateTime ContractStart { get; set; }
        public DateTime ContractEnd { get; set; }
        public bool IsHod { get; set; }
        public int UserId { get; set; }

        public virtual Department Department { get; set; }
        public virtual Position Position { get; set; }
        public virtual User User { get; set; }
    }
}
