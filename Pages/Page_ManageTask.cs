using EmployeeManagement.DAL.Interfaces;
using EmployeeManagement.DAL.Repositories;
using Task = EmployeeManagement.Models.Task;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using AntdUI;

namespace EmployeeManagement.Pages
{
    public partial class Page_ManageTask : UserControl
    {
        public TaskRepository _taskRepository = new TaskRepository();

        public Page_ManageTask()
        {
            InitializeComponent();
        }

        public void Page_ManageTask_Load(int id)
        {
            tbTask.Columns.Add(new Column("TaskID", "ID"));
            tbTask.Columns.Add(new Column("ProjectID", "Project ID"));
            tbTask.Columns.Add(new Column("TaskTitle", "Task Title"));
            tbTask.Columns.Add(new Column("Description", "Description"));
            tbTask.Columns.Add(new Column("AssignedTo", "Assigned To"));
            tbTask.Columns.Add(new Column("CreateBy", "CreateBy"));
            tbTask.Columns.Add(new Column("Deadline", "Deadline"));
            tbTask.Columns.Add(new Column("Status", "Status"));
            tbTask.Columns.Add(new Column("Priority", "Priority"));
            tbTask.Columns.Add(new Column("Progress", "Progress"));
            tbTask.Columns.Add(new Column("CreatedDate", "Created Date"));
            tbTask.Columns.Add(new Column("UpdateDate", "Update Date"));

            tbTask.DataSource = _taskRepository.GetByProject(id);
        }
    }
}
