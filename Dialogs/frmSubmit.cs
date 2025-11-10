using AntdUI;
using EmployeeManagement.DAL.Repositories;
using EmployeeManagement.DAL.Services;
using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
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
    public partial class frmSubmit : Form
    {
        private MyTask _task;
        private int? _projectID;
        private int _currentEmployeeID;
        private ErrorProvider _errorProvider;

        public frmSubmit(MyTask task, int currentEmployeeID)
        {
            InitializeComponent();
            _task = task;
            _currentEmployeeID = currentEmployeeID;

            _errorProvider = new ErrorProvider();
            _errorProvider.BlinkStyle = ErrorBlinkStyle.NeverBlink;
        }

        private void frmSubmit_Load(object sender, EventArgs e)
        {
            ddownStatus.Items.Clear();
            ddownStatus.Items.Add("Chưa làm");
            ddownStatus.Items.Add("Đã làm");

            if (_projectID.HasValue)
            {
                _task = new MyTask();
                _task.ProjectID = _projectID.Value;
                ddownProjectID.Text = _projectID.ToString();
                ddownProjectID.Enabled = false;
                ddownStatus.Text = "Chưa làm";
            }
            else if (_task != null)
            {
                txtMaNguoiTao.Enabled = false;
                ddownProjectID.Text = _task.ProjectID.ToString();
                txtTaskName.Text = _task.TaskName;
                txtTaskName.Enabled = false;
                txtDescription.Text = _task.Description;
                txtDescription.Enabled = false;
                txtMaNguoiTao.Text = _task.CreatedBy.ToString();
                txtMaNguoiTao.Enabled = false;
                dateEnd.Text = _task.Deadline?.ToString("yyyy-MM-dd") ?? "Chưa có";
                dateEnd.Enabled = false;
                ddownProjectID.Enabled = false;

                TaskRepository taskRepository = new TaskRepository();
                string currentStatus = taskRepository.GetAssignmentStatus(_task.TaskID, _currentEmployeeID);
                ddownStatus.Text = currentStatus == "Completed" ? "Đã làm" : "Chưa làm";
            }

            loadProjectsID();

            ddownProjectID.Validated += (s, ev) => _errorProvider.SetError(ddownProjectID, string.Empty);
            txtTaskName.Validated += (s, ev) => _errorProvider.SetError(txtTaskName, string.Empty);
            ddownStatus.Validated += (s, ev) => _errorProvider.SetError(ddownStatus, string.Empty);
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

            if (ddownProjectID.Items.Count > 0 && string.IsNullOrWhiteSpace(ddownProjectID.Text))
            {
                ddownProjectID.SelectedValue = null;
            }
        }

        private EmployeeRepository employeeRepository = new EmployeeRepository();

        private TaskRepository taskRepository = new TaskRepository();

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
                if (!ValidateForm())
                {
                    return;
                }

                string newStatus = ddownStatus.Text == "Đã làm" ? "Completed" : "Pending";
                TaskRepository taskRepository = new TaskRepository();
                bool success = taskRepository.UpdateAssignmentStatus(_task.TaskID, _currentEmployeeID, newStatus);
                if (success)
                {
                    Message.success(this.FindForm(), "Cập nhật trạng thái thành công!");
                    this.DialogResult = DialogResult.OK;
                    this.Close();
                }
                else
                {
                    Message.error(this.FindForm(), "Lỗi khi cập nhật trạng thái!");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi cập nhật trạng thái: " + ex.Message);
            }
        }

        private void ddownProjectID_SelectedValueChanged(object sender, AntdUI.ObjectNEventArgs e)
        {
            if (e.Value != null)
            {
                ddownProjectID.Text = e.Value.ToString();
            }
            else
            {
                ddownProjectID.Text = string.Empty;
            }
        }

        private void ddownStatus_SelectedValueChanged(object sender, AntdUI.ObjectNEventArgs e)
        {
            if (e.Value != null)
            {
                ddownStatus.Text = e.Value.ToString();
            }
            else
            {
                ddownStatus.Text = string.Empty;
            }
        }

        private void uploadDragger1_DragChanged(object sender, StringsEventArgs e)
        {
            try
            {
                if (e.Value == null || e.Value.Length == 0)
                {
                    return;
                }

                if (_task == null || _task.TaskID <= 0)
                {
                    Message.error(this.FindForm(), "Invalid Task ID. Cannot upload file.");
                    return;
                }

                FileService fileService = new FileService();
                int successCount = 0;
                int failCount = 0;

                foreach (string filePath in e.Value)
                {
                    if (!File.Exists(filePath))
                    {
                        failCount++;
                        continue;
                    }

                    string title = Path.GetFileNameWithoutExtension(filePath);

                    if (string.IsNullOrWhiteSpace(title) || title.Length > 200)
                    {
                        failCount++;
                        continue;
                    }

                    bool success = fileService.UploadTaskFile(_task.TaskID, title, filePath, _currentEmployeeID);

                    if (success)
                    {
                        successCount++;
                    }
                    else
                    {
                        failCount++;
                    }
                }

                if (failCount == 0)
                {
                    Message.success(this.FindForm(), $"Đã tải lên thành công {successCount} file!");
                }
                else
                {
                    Message.warn(this.FindForm(), $"Tải lên thành công {successCount} file, thất bại {failCount} file!");
                }
            }
            catch (Exception ex)
            {
                Message.error(this.FindForm(), "Lỗi khi tải lên file: " + ex.Message);
            }
        }

        private bool ValidateForm()
        {
            _errorProvider.Clear();
            bool valid = true;

            if (string.IsNullOrWhiteSpace(ddownProjectID.Text) || !int.TryParse(ddownProjectID.Text, out int projId))
            {
                _errorProvider.SetError(ddownProjectID, "Chọn Project hợp lệ");
                valid = false;
            }

            if (_task == null || _task.TaskID <= 0)
            {
                Message.error(this.FindForm(), "Task chưa tồn tại hoặc Task ID không hợp lệ. Không thể cập nhật trạng thái.");
                valid = false;
            }

            if (string.IsNullOrWhiteSpace(txtTaskName.Text))
            {
                _errorProvider.SetError(txtTaskName, "Tên task không được để trống");
                valid = false;
            }

            if (string.IsNullOrWhiteSpace(ddownStatus.Text) || !(ddownStatus.Text == "Chưa làm" || ddownStatus.Text == "Đã làm"))
            {
                _errorProvider.SetError(ddownStatus, "Chọn trạng thái: 'Chưa làm' hoặc 'Đã làm'");
                valid = false;
            }

            if (!string.IsNullOrWhiteSpace(txtDescription.Text) && txtDescription.Text.Length > 2000)
            {
                _errorProvider.SetError(txtDescription, "Mô tả quá dài (tối đa 2000 ký tự)");
                valid = false;
            }

            return valid;
        }
    }
}
