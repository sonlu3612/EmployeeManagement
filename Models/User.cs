using System;

namespace EmployeeManagement.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string Role { get; set; } // Admin, Manager, Employee
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}