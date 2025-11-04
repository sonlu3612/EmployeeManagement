using EmployeeManagement.DAL.Repositories;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using MyTask = EmployeeManagement.Models.Task;

namespace EmployeeManagement.Dialogs
{
    public partial class frmTask : Form
    {
        private MyTask _task;

        
        public frmTask(MyTask task)
        {
            InitializeComponent();
            _task = task;
        }

        private void frmTask_Load(object sender, EventArgs e)
        {
            if (_task == null)
            {
                _task = new MyTask();
            }
            txtTaskName.Text = _task.TaskName;
            ddownProjectID.Text = _task.ProjectID.ToString();
            txtDescription.Text = _task.Description;
            ddownOwnerID.Text = _task.AssignedTo.HasValue ? _task.AssignedTo.Value.ToString() : "";
            ddownStatus.Text = _task.Status;
            dateStart.Value = _task.CreatedDate;
            dateEnd.Value = _task.Deadline;

            loadProjectsID();
            loadEmployeesID();

        }

        private ProjectRepository projectRepository = new ProjectRepository();
        private void loadProjectsID()
        {
            var IDs = projectRepository.GetAll();
            ddownProjectID.Items.Clear();
            foreach (var id in IDs)
            {
                ddownProjectID.Items.Add(id.ProjectID);
            }
        }

        private EmployeeRepository employeeRepository = new EmployeeRepository();
        private void loadEmployeesID()
        {
            var IDs = employeeRepository.GetAll();
            ddownOwnerID.Items.Clear();
            foreach (var id in IDs)
            {
                ddownOwnerID.Items.Add(id.EmployeeID);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            frmInforHuy frmInforHuy = new frmInforHuy("Bạn có chắc muốn hủy các thay đổi?", this);
            this.Close();

        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                if(_task.TaskID == 0)
                {
                    var indertTask = new MyTask
                    {
                        TaskID = _task.TaskID,
                        TaskName = txtTaskName.Text,
                        ProjectID = Convert.ToInt32(ddownProjectID.SelectedValue.ToString()),
                        Description = txtDescription.Text,
                        AssignedTo = string.IsNullOrEmpty(ddownOwnerID.Text) ? (int?)null : Convert.ToInt32(ddownOwnerID.SelectedValue.ToString()),
                        Status = "Todo",
                        CreatedDate = DateTime.Now,
                        Priority = ddownPriority.Text,
                        CreatedBy = 2
                    };
                    TaskRepository taskRepository = new TaskRepository();
                    taskRepository.Insert(indertTask);
                    frmInforHuy frmInforHuy = new frmInforHuy("Thêm nhiệm vụ thành công!", this);
                    frmInforHuy.Show();
                    return;
                }
                else 
                {
                    var updateTask = new MyTask
                    {
                        TaskID = _task.TaskID,
                        TaskName = txtTaskName.Text,
                        ProjectID = Convert.ToInt32(ddownProjectID.Text),
                        Description = txtDescription.Text,
                        AssignedTo = string.IsNullOrEmpty(ddownOwnerID.Text) ? (int?)null : Convert.ToInt32(ddownOwnerID.Text),
                        Status = ddownStatus.Text,
                        //CreatedDate = DateTime.Now
                        CreatedDate = _task.CreatedDate,
                        Priority = ddownPriority.Text,
                        Deadline = dateEnd.Value
                    };

                    TaskRepository taskRepository = new TaskRepository();
                    taskRepository.Update(updateTask);
                    frmInforHuy frmInforHuy = new frmInforHuy("Cập nhật nhiệm vụ thành công!", this);
                    frmInforHuy.Show();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật nhiệm vụ: " + ex.Message);
            }
        }

        private void ddownProjectID_SelectedValueChanged(object sender, AntdUI.ObjectNEventArgs e)
        {
            ddownProjectID.Text = ddownProjectID.SelectedValue.ToString();
        }

        private void ddownOwnerID_SelectedValueChanged(object sender, AntdUI.ObjectNEventArgs e)
        {
            ddownOwnerID.Text = ddownOwnerID.SelectedValue.ToString();
        }

        private void ddownStatus_SelectedValueChanged(object sender, AntdUI.ObjectNEventArgs e)
        {
            ddownStatus.Text = ddownStatus.SelectedValue.ToString();
        }

        private void ddownPriority_SelectedValueChanged(object sender, AntdUI.ObjectNEventArgs e)
        {
            ddownPriority.Text = ddownPriority.SelectedValue.ToString();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
