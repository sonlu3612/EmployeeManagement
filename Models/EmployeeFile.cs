using System;

namespace EmployeeManagement.Models
{
    public class EmployeeFile
    {
        public int EmployeeFileID { get; set; }
        public int EmployeeID { get; set; }
        public string Title { get; set; }
        public string FileName { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; } // For display
        public string EmployeeName { get; set; }
        public string CreatedByName { get; set; }
    }
}