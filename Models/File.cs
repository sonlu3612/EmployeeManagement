using System;

namespace EmployeeManagement.Models
{
    public class File
    {
        public int FileID { get; set; }
        public string Title { get; set; }
        public string FileName { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }  // For display
        public string CreatedByName { get; set; }
    }
}