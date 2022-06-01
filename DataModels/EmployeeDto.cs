using System;

namespace Liberty.DataModels
{
    public class EmployeeDto
    {
        public int UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string MobileNumber { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public int EmploymentDetailsId { get; set; }
        public string EmploymentNumber { get; set; }
        public int PositionId { get; set; }
        public int DepartmentId { get; set; }
        public DateTime ContractStart { get; set; }
        public DateTime ContractEnd { get; set; }
        public bool IsHod { get; set; }

    }
}