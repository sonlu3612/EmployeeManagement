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
            else if (_projectID.HasValue)
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
            else if (_task != null)
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
        // var IDs = employeeRepository.GetAll();
        // ddownOwnerID.Items.Clear();
        // foreach (var id in IDs)
        // {
        // ddownOwnerID.Items.Add(id.EmployeeID+"-"+id.FullName);
        // }
        //}
        private TaskRepository taskRepository = new TaskRepository();
        private void loadPriority()
        {
            var Prioritys = taskRepository.GetAll();
            ddownPriority.Items.Clear();
            foreach (var id in Prioritys)
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
                string taskName = txtTaskName?.Text?.Trim() ?? string.Empty;
                string projectIdText = ddownProjectID?.Text?.Trim() ?? string.Empty;
                string description = txtDescription?.Text?.Trim() ?? string.Empty;
                string createdByText = txtMaNguoiTao?.Text?.Trim() ?? string.Empty;
                string status = ddownStatus?.Text?.Trim() ?? string.Empty;
                string priority = ddownPriority?.Text?.Trim() ?? string.Empty;
                DateTime createdDate = DateTime.Parse(dateStart?.Text ?? DateTime.Now.ToString("yyyy-MM-dd"));
                DateTime deadline = dateEnd?.Value ?? DateTime.Now;

                if (string.IsNullOrWhiteSpace(taskName))
                {
                    Message.error(this.FindForm(), "Vui lòng nhập tên nhiệm vụ!");
                    return;
                }
                if (taskName.Length > 200)
                {
                    Message.error(this.FindForm(), "Tên nhiệm vụ quá dài (tối đa 200 ký tự).");
                    return;
                }
                if (string.IsNullOrWhiteSpace(projectIdText) || !int.TryParse(projectIdText, out int projectId))
                {
                    Message.error(this.FindForm(), "Vui lòng chọn dự án hợp lệ!");
                    return;
                }
                if (string.IsNullOrWhiteSpace(status))
                {
                    Message.error(this.FindForm(), "Vui lòng chọn trạng thái!");
                    return;
                }
                if (string.IsNullOrWhiteSpace(priority))
                {
                    Message.error(this.FindForm(), "Vui lòng chọn mức độ ưu tiên!");
                    return;
                }
                if (deadline < createdDate)
                {
                    Message.error(this.FindForm(), "Ngày kết thúc phải lớn hơn hoặc bằng ngày bắt đầu!");
                    return;
                }
                if (string.IsNullOrWhiteSpace(createdByText) || !int.TryParse(createdByText, out int createdBy))
                {
                    Message.error(this.FindForm(), "Mã người tạo không hợp lệ!");
                    return;
                }

                if (_task.TaskID == 0)
                {
                    var insertTask = new MyTask();

                    if (!_projectID.HasValue)
                    {
                        insertTask = new MyTask
                        {
                            TaskID = _task.TaskID,
                            TaskName = taskName,
                            ProjectID = Convert.ToInt32(ddownProjectID.SelectedValue.ToString()),
                            Description = description,
                            CreatedBy = createdBy,
                            Status = status,
                            CreatedDate = DateTime.Now,
                            Priority = priority,
                            Deadline = deadline

                        };
                    }
                    else
                    {
                        insertTask = new MyTask
                        {
                            TaskID = _task.TaskID,
                            TaskName = taskName,
                            ProjectID = _projectID.Value,
                            Description = description,
                            CreatedBy = createdBy,
                            Status = status,
                            CreatedDate = DateTime.Now,
                            Priority = priority,
                            Deadline = deadline

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
                        TaskName = taskName,
                        ProjectID = Convert.ToInt32(ddownProjectID.Text),
                        Description = description,
                        CreatedBy = createdBy,
                        Status = status,
                        //CreatedDate = DateTime.Now
                        CreatedDate = _task.CreatedDate,
                        Priority = priority,
                        Deadline = deadline
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
                Message.error(this.FindForm(), "Lỗi khi cập nhật nhiệm vụ: " + ex.Message);
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
    }
}