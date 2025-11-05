using AntdUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EmployeeManagement.DAL.Repositories;

namespace EmployeeManagement.Pages
{
    public partial class Page_AssignTask : UserControl
    {
 
        public Page_AssignTask()
        {
            InitializeComponent();
        }

        private TaskRepository repository = new TaskRepository();
        private int EmployeeID;

        private void Page_AssignTask_Load(object sender, EventArgs e)
        {
            tbTask.Columns.Add(new Column("TaskID", "ID"));
            tbTask.Columns.Add(new Column("ProjectName", "Tên dự án"));
            tbTask.Columns.Add(new Column("TaskName", "Tên nhiệm vụ"));
            tbTask.Columns.Add(new Column("Description", "Mô tả"));
            tbTask.Columns.Add(new Column("CreateBy", "Người tạo"));
            tbTask.Columns.Add(new Column("Deadline", "Đến hạn"));
            tbTask.Columns.Add(new Column("Status", "Trạng thái"));
            tbTask.Columns.Add(new Column("Priority", "Độ ưu tiên"));
            tbTask.Columns.Add(new Column("Progress", "Tiến triển"));
        }

        public void Page_AssignTask_Load(int id)
        {
            this.EmployeeID = id;
            tbTask.Columns.Add(new Column("TaskID", "ID"));
            tbTask.Columns.Add(new Column("ProjectName", "Tên dự án"));
            tbTask.Columns.Add(new Column("TaskName", "Tên nhiệm vụ"));
            tbTask.Columns.Add(new Column("Description", "Mô tả"));
            tbTask.Columns.Add(new Column("CreateBy", "Người tạo"));
            tbTask.Columns.Add(new Column("Deadline", "Đến hạn"));
            tbTask.Columns.Add(new Column("Status", "Trạng thái"));
            tbTask.Columns.Add(new Column("Priority", "Độ ưu tiên"));
            tbTask.Columns.Add(new Column("Progress", "Tiến triển"));

            LoadData();
        }

        private void LoadData()
        {
            tbTask.DataSource = repository.GetByEmployee(this.EmployeeID);
        }
    }
}
