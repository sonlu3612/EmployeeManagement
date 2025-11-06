using System;
using System.Collections.Generic;

namespace EmployeeManagement.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        public string FullName { get; set; }
        public string Position { get; set; }
        public string Gender { get; set; }
        public int? DepartmentID { get; set; }
        public string DepartmentName { get; set; } // For display
        public string AvatarPath { get; set; }
        public string Address { get; set; }
        public DateTime HireDate { get; set; }
        public bool IsActive { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }
        public string ProjectSummary { get; set; } // dạng "2/5"
        public string TaskSummary { get; set; }    // dạng "4/10"
        public byte[] AvatarData { get; set; }     // dữ liệu ảnh thực tế
    }
}