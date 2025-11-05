using AntdUI;
using EmployeeManagement.DAL.Repositories;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using Message = AntdUI.Message;
using MyTask = EmployeeManagement.Models.Task;

namespace EmployeeManagement.Dialogs
{
    public partial class frmTask : Form
    {
        private MyTask _task;
        private int _projectID;

        public frmTask(int projectId)
        {
            InitializeComponent();
            _projectID = projectId;
        }
        
        public frmTask(MyTask task)
        {
            InitializeComponent();
            _task = task;
        }

        private void frmTask_Load(object sender, EventArgs e)
        {
            if (_task == null && _projectID == null)
            {
                _task = new MyTask();
                ddownProjectID.Enabled = true;
            }
            else if(_projectID != null)
            {
                _task = new MyTask();
                _task.ProjectID = _projectID;
                ddownProjectID.Text = _projectID.ToString();
                ddownProjectID.Enabled = false;
            }
            else if( _task != null)
            {
                txtTaskName.Text = _task.TaskName;
                ddownProjectID.Text = _task.ProjectID.ToString();
                txtDescription.Text = _task.Description;
                ddownOwnerID.Text = _task.CreatedBy.HasValue ? _task.CreatedBy.Value.ToString() : "";
                ddownStatus.Text = _task.Status;
                dateStart.Text = _task.CreatedDate.ToString();
                ddownPriority.Text = _task.Priority;
                dateEnd.Text = _task.Deadline.ToString();
                ddownProjectID.Enabled = false;

            }

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
                ddownOwnerID.Items.Add(id.EmployeeID+"-"+id.FullName);
            }
        }

        private TaskRepository taskRepository = new TaskRepository();
        private void loadPriority()
        {
            var Prioritys = taskRepository.GetAll();
            ddownPriority.Items.Clear();
            foreach(var id in Prioritys)
            {
                ddownPriority.Items.Add(id.Priority);
            }
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            var modalConfig = AntdUI.Modal.config(
                this.FindForm(),
                "Xác nhận hủy",
                "Bạn có muốn hủy?",
                TType.Warn
            );
            modalConfig.OkText = "Hủy";
            modalConfig.CancelText = "Thoát";
            modalConfig.OkType = TTypeMini.Primary;
            modalConfig.OnOk = (cfg) =>
            {
                return true;
            };
            AntdUI.Modal.open(modalConfig);
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            try
            {
                if(_task.TaskID == 0)
                {
                    var insertTask = new MyTask();
                    ddownStatus.Text = "Cần làm";
                    dateStart.Text = DateTime.Now.ToString("yyyy-MM-dd");
                    if(_projectID == null)
                    {
                        int employeeID = int.Parse(ddownOwnerID.Text.Split('-')[0].Trim());


                        insertTask = new MyTask
                        {
                            TaskID = _task.TaskID,
                            TaskName = txtTaskName.Text,
                            ProjectID = Convert.ToInt32(ddownProjectID.SelectedValue.ToString()),
                            Description = txtDescription.Text,
                            AssignedTo = employeeID,
                            Status = ddownStatus.Text,
                            CreatedDate = DateTime.Now,
                            Priority = ddownPriority.Text,
                            CreatedBy = 2
                        };

                    }
                    else
                    {
                        int employeeID = int.Parse(ddownOwnerID.Text.Split('-')[0].Trim());

                        insertTask = new MyTask
                        {
                            TaskID = _task.TaskID,
                            TaskName = txtTaskName.Text,
                            ProjectID = _projectID,
                            Description = txtDescription.Text,
                            AssignedTo = employeeID,
                            Status = "Cần làm",
                            CreatedDate = DateTime.Now,
                            Priority = ddownPriority.Text,
                            CreatedBy = 2
                        };

                    }

                    try
                    {
                        TaskRepository taskRepository = new TaskRepository();
                        taskRepository.Insert(insertTask);
                        Message.success(this.FindForm(), "Thêm nhiệm vụ thành công!");
                        this.DialogResult = DialogResult.OK;
                        this.Close();

                    }
                    catch (Exception ex) 
                    {
                        Message.error(this.FindForm(), "Lỗi không thể thêm nhiệm vụ!");
                    }
                }
                else 
                {
                    int employeeID = int.Parse(ddownOwnerID.Text.Split('-')[0].Trim());

                    var updateTask = new MyTask
                    {
                        TaskID = _task.TaskID,
                        TaskName = txtTaskName.Text,
                        ProjectID = Convert.ToInt32(ddownProjectID.Text),
                        Description = txtDescription.Text,
                        AssignedTo = employeeID,
                        Status = ddownStatus.Text,
                        //CreatedDate = DateTime.Now
                        CreatedDate = _task.CreatedDate,
                        Priority = ddownPriority.Text,
                        Deadline = dateEnd.Value
                    };

                    TaskRepository taskRepository = new TaskRepository();
                    taskRepository.Update(updateTask);
                    Message.success(this.FindForm(), "Cập nhật nhiệm vụ thành công!");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
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
