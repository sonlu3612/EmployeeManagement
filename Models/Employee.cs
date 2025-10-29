using System;

namespace EmployeeManagement.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public int? DepartmentID { get; set; }
        public string DepartmentName { get; set; } // For display
        public string ImagePath { get; set; }
        public string Address { get; set; }
        public DateTime HireDate { get; set; }
        public bool IsActive { get; set; }
    }
}
