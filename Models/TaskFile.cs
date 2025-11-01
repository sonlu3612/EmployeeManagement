using System;

namespace EmployeeManagement.Models
{
    public class TaskFile
    {
        public int TaskFileID { get; set; }
        public int TaskID { get; set; }
        public string Title { get; set; }
        public string FileName { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }// For display

        public string TaskTitle { get; set; }
        public string CreatedByName { get; set; }
    }
}