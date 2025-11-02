using System;

namespace EmployeeManagement.Models
{
    public class ProjectFile
    {
        public int ProjectFileID { get; set; }
     public int ProjectID { get; set; }
        public string Title { get; set; }
    public string FileName { get; set; }
        public int CreatedBy { get; set; }
        public DateTime CreatedAt { get; set; }
    
        // For display
        public string ProjectName { get; set; }
    public string CreatedByName { get; set; }
  }
}
