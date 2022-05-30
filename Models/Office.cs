using System;
using System.Collections.Generic;

#nullable disable

namespace Liberty.Models
{
    public partial class Office
    {
        public Office()
        {
            Departments = new HashSet<Department>();
        }

        public int OfficeId { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public string Status { get; set; }

        public virtual ICollection<Department> Departments { get; set; }
    }
}
