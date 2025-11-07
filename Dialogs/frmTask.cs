using AntdUI;
using EmployeeManagement.DAL.Helpers;
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
using System.Web.ModelBinding;
using System.Windows.Forms;
using static System.ComponentModel.Design.ObjectSelectorEditor;
using Message = AntdUI.Message;
using MyTask = EmployeeManagement.Models.Task;

namespace EmployeeManagement.Dialogs
{
    public partial class frmTask : Form
    {
        private MyTask _task;
        private int? _projectID;

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
            if (_task == null && !_projectID.HasValue)
            {   
                _task = new MyTask();
                ddownProjectID.Enabled = true;
                ddownStatus.Text = "Cần làm";
                dateStart.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
            else if(_projectID.HasValue)
            {
                _task = new MyTask();
                _task.ProjectID = _projectID.Value;
                ddownProjectID.Text = _projectID.ToString();
                ddownProjectID.Enabled = false;
                ddownStatus.Text = "Cần làm";
                txtMaNguoiTao.Text = SessionManager.CurrentUser.UserID.ToString();
                txtMaNguoiTao.Enabled = false;
                dateStart.Text = DateTime.Now.ToString("yyyy-MM-dd");
            }
            else if( _task != null)
            {
                txtMaNguoiTao.Enabled = false;
                dateStart.Text = _task.CreatedDate.ToString("yyyy-MM-dd");
                dateStart.Enabled = false;
                ddownProjectID.Text = _task.ProjectID.ToString();
               
                txtMaNguoiTao.Text = _task.CreatedBy.ToString();
                txtMaNguoiTao.Enabled = false;
                txtTaskName.Text = _task.TaskName;
                //ddownProjectID.SelectedValue = _task.ProjectID.ToString();
                txtDescription.Text = _task.Description;
                txtMaNguoiTao.Text = _task.CreatedBy.ToString();
                ddownStatus.Text = _task.Status;
                dateStart.Text = _task.CreatedDate.ToString();
                ddownPriority.Text = _task.Priority;
                dateEnd.Value = _task.Deadline;
                ddownProjectID.Enabled = false;

            }

            loadProjectsID();
            //loadEmployeesID();

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
        //private void loadEmployeesID()
        //{
        //    var IDs = employeeRepository.GetAll();
        //    ddownOwnerID.Items.Clear();
        //    foreach (var id in IDs)
        //    {
        //        ddownOwnerID.Items.Add(id.EmployeeID+"-"+id.FullName);
        //    }
        //}

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
            modalConfig.CancelText = "Tiếp tục";
            modalConfig.OkType = TTypeMini.Primary;
            modalConfig.OnOk = (cfg) =>
            {
                this.DialogResult = DialogResult.Cancel; // ĐÁNH DẤU HỦY
                this.Close();
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
                    
                    if(!_projectID.HasValue)
                    {
                        insertTask = new MyTask
                        {
                            TaskID = _task.TaskID,
                            TaskName = txtTaskName.Text,
                            ProjectID = Convert.ToInt32(ddownProjectID.SelectedValue.ToString()),
                            Description = txtDescription.Text,
                            CreatedBy = int.Parse(txtMaNguoiTao.Text),
                            Status = ddownStatus.Text,
                            CreatedDate = DateTime.Now,
                            Priority = ddownPriority.Text,
                            Deadline = dateEnd.Value
                          
                        };

                    }
                    else
                    {
                        insertTask = new MyTask
                        {
                            TaskID = _task.TaskID,
                            TaskName = txtTaskName.Text,
                            ProjectID = _projectID.Value,
                            Description = txtDescription.Text,
                            CreatedBy = int.Parse(txtMaNguoiTao.Text),
                            Status = ddownStatus.Text,
                            CreatedDate = DateTime.Now,
                            Priority = ddownPriority.Text,
                            Deadline = dateEnd.Value
                            
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
                    catch (Exception) 
                    {
                        Message.error(this.FindForm(), "Lỗi không thể thêm nhiệm vụ!");
                    }
                }
                else 
                {
                    
                   
                    var updateTask = new MyTask
                    {
                        TaskID = _task.TaskID,
                        TaskName = txtTaskName.Text,
                        ProjectID = Convert.ToInt32(ddownProjectID.Text),
                        Description = txtDescription.Text,
                        CreatedBy = int.Parse(txtMaNguoiTao.Text),
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
