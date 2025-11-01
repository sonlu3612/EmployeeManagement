using AntdUI;
using EmployeeManagement.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;



namespace EmployeeManagement.Pages
{
    public partial class Page_Task : UserControl
    {
        public Page_Task()
        {
            InitializeComponent();
        }

        private TaskRepository taskRepository = new TaskRepository();
        private void Page_Task_Load(object sender, EventArgs e)
        {
            tableTask.Columns.Add(new Column("Dự án", "ProjectName"));
            tableTask.Columns.Add(new Column("Nhiệm vụ", "TaskName"));
            tableTask.Columns.Add(new Column("Tạo bởi", "EmployeeName"));
            tableTask.Columns.Add(new Column("Ngày tạo", "CreatedDate"));
            tableTask.Columns.Add(new Column("Hạn hoàn thành", "Deadline"));
            tableTask.Columns.Add(new Column("Trạng thái", "Status"));
            tableTask.Columns.Add(new Column("Tiến triển", "Progress"));
            tableTask.Columns.Add(new Column("Độ ưu tiên", "Priority"));

            loadData();

        }

        private void loadData()
        {
             try 
             {
                var tasks = taskRepository.GetAll();

                var data = tasks.Select(t => new
                {
                    ProjectName = t.ProjectName,
                    TaskName = t.TaskName,
                    EmployeeName = t.EmployeeName, // người tạo / nhân viên phụ trách
                    CreatedDate = t.CreatedDate.ToString("dd/MM/yyyy"),
                    Deadline = t.Deadline?.ToString("dd/MM/yyyy") ?? "—",
                    Status = t.Status,
                    Progress = t.Progress + "%",
                    Priority = t.Priority
                }).ToList<object>();


                tableTask.DataSource = data;
                
            }
             catch (Exception ex)
             {
                 MessageBox.Show("Error loading tasks: " + ex.Message);
             }

        }

    }
}
   