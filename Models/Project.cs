using System;

namespace EmployeeManagement.Models
{
    public class Project
    {
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public string Description { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Status { get; set; }
        public int CreatedBy { get; set; }
        public string CreatedByName { get; internal set; }
        public int? ManagerBy { get; set; }
        public string ManagerName { get; internal set; }
    }
}