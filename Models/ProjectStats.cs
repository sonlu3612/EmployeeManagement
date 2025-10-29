namespace EmployeeManagement.Models
{
    public class ProjectStats
    {
        public int ProjectID { get; set; }
        public string ProjectName { get; set; }
        public int TotalTasks { get; set; }
        public int CompletedTasks { get; set; }
        public decimal CompletionPercentage { get; set; }
    }
}
