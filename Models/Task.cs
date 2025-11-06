using System;
using System.Collections.Generic;
namespace EmployeeManagement.Models
{
    public class Task
    {
        public int TaskID { get; set; }
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public string TaskName { get; set; }
        public string Description { get; set; }
        public int CreatedBy { get; set; }
        public string EmployeeName { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public DateTime? Deadline { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdatedDate { get; set; }
        public List<int> AssignedEmployeeIds { get; set; } = new List<int>(); // List ID employees assigned
        public List<string> AssignedEmployeeNames { get; set; } = new List<string>();
    }
}