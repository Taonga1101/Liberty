using System;
using System.Collections.Generic;

#nullable disable

namespace Liberty.Models
{
    public partial class Position
    {
        public Position()
        {
            EmploymentDetails = new HashSet<EmploymentDetail>();
        }

        public int PositionId { get; set; }
        public string Name { get; set; }

        public virtual ICollection<EmploymentDetail> EmploymentDetails { get; set; }
    }
}
