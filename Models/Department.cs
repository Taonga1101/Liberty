using System;
using System.Collections.Generic;

#nullable disable

namespace Liberty.Models
{
    public partial class Department
    {
        public Department()
        {
            EmploymentDetails = new HashSet<EmploymentDetail>();
        }

        public int DepartmentId { get; set; }
        public int OfficeId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Status { get; set; }

        public virtual Office Office { get; set; }
        public virtual ICollection<EmploymentDetail> EmploymentDetails { get; set; }
    }
}
