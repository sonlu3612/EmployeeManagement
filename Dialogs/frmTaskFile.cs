using EmployeeManagement.DAL.Repositories;
using EmployeeManagement.Pages;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeeManagement.Dialogs
{
    public partial class frmTaskFile : Form
    {
        private int _taskId;
        private Page_TaskFile ucTaskFile;

        public frmTaskFile(int taskID)
        {
            InitializeComponent();
            _taskId = taskID;
        }

        private void frmTaskFile_Load(object sender, EventArgs e)
        {
            var repo = new TaskRepository();
            var task = repo.GetById(_taskId);
            string taskName = task?.TaskName ?? "Unknown Task";
            page_TaskFile1.LoadTaskFiles(_taskId, taskName);
        }
    }
}