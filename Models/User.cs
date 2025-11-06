using System;
using System.Collections.Generic;

namespace EmployeeManagement.Models
{
    public class User
    {
        public int UserID { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public List<string> Roles { get; set; } = new List<string>();
        public bool IsActive { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}