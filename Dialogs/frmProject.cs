using EmployeeManagement.DAL.Helpers;
using EmployeeManagement.DAL.Interfaces;
using EmployeeManagement.DAL.Repositories;
using EmployeeManagement.Models;
using System;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Message = AntdUI.Message;
namespace EmployeeManagement.Dialogs
{
    public partial class frmProject : Form
    {
        private readonly Project _project;
        private readonly bool _isEdit;
        private IRepository<Project> _projectRepository = new ProjectRepository();
        private User currentUser = SessionManager.CurrentUser;
        private EmployeeRepository _employeeRepository = new EmployeeRepository();
        public frmProject(Project project = null)
        {
            InitializeComponent();
            _project = project;
            _isEdit = project != null;
        }
        private bool IsInDesignMode()
        {
            return LicenseManager.UsageMode == LicenseUsageMode.Designtime
                   || (this.Site != null && this.Site.DesignMode);
        }
        private bool IsAdmin()
        {
            return SessionManager.CurrentUser?.Roles?.Contains("Admin") ?? false;
        }
        private bool IsProjectManager()
        {
            return SessionManager.CurrentUser?.Roles?.Contains("Quản lý dự án") ?? false;
        }
        private bool IsEmployee()
        {
            return SessionManager.CurrentUser?.Roles?.Contains("Nhân viên") ?? false;
        }
        private void frmProject_Load(object sender, EventArgs e)
        {
            loadEmployeesName();
            if (IsAdmin())
            {
                loadData();
            }
            else if (IsProjectManager())
            {
                loadData();
                cbManager.Enabled = false;
            }
            else
            {
                loadData();
                txtProjectName.Enabled = false;
                txtDescription.Enabled = false;
                dtStartDate.Enabled = false;
                dtEndDate.Enabled = false;
                cbManager.Enabled = false;
                cboStatus.Enabled = false;
            }
        }
        private void loadData()
        {
            if (_isEdit)
            {
                txtProjectName.Text = _project.ProjectName;
                txtDescription.Text = _project.Description;
                dtStartDate.Value = _project.StartDate;
                dtEndDate.Value = _project.EndDate;
                cbManager.Text = _project.CreatedBy.ToString();
                cbManager.Text = _project.ManagerBy.ToString() + " - " + _project.ManagerName;
                cbManager.SelectedValue = _project.ManagerBy;
                cboStatus.Text = _project.Status;
            }
            else
            {
                txtProjectName.Clear();
                txtDescription.Clear();
                dtStartDate.Value = DateTime.Now;
                dtEndDate.Value = DateTime.Now;
                cbManager.SelectedValue = currentUser.UserID;
                cboStatus.Text = "Đang kế hoạch";
                cboStatus.SelectedValue = cboStatus.Text;
            }
        }
        private void loadEmployeesName()
        {
            var employees = _employee_repository_getall();
            cbManager.Items.Clear();
            cbManager.Items.Insert(0, "Tất cả");
            foreach (var emp in employees)
            {
                string displayText = $"{emp.EmployeeID} - {emp.FullName}";
                cbManager.Items.Add(displayText);
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                string projectName = txtProjectName?.Text?.Trim() ?? string.Empty;
                string description = txtDescription?.Text?.Trim() ?? string.Empty;
                DateTime startDate = dtStartDate?.Value ?? DateTime.Now;
                DateTime endDate = dtEndDate?.Value ?? DateTime.Now;
                string status = cboStatus?.Text?.Trim() ?? string.Empty;
                string managerText = cbManager?.Text?.Trim() ?? string.Empty;
                if (string.IsNullOrWhiteSpace(projectName))
                {
                    Message.error(this.FindForm(), "Vui lòng nhập tên dự án!");
                    return;
                }
                if (projectName.Length > 200)
                {
                    Message.error(this.FindForm(), "Tên dự án quá dài (tối đa 200 ký tự).");
                    return;
                }
                if (string.IsNullOrWhiteSpace(status))
                {
                    Message.error(this.FindForm(), "Vui lòng chọn trạng thái dự án!");
                    return;
                }
                if (endDate < startDate)
                {
                    Message.error(this.FindForm(), "Ngày kết thúc phải lớn hơn hoặc bằng ngày bắt đầu!");
                    return;
                }
                if (string.IsNullOrWhiteSpace(managerText) || managerText.Equals("Tất cả", StringComparison.OrdinalIgnoreCase))
                {
                    Message.error(this.FindForm(), "Vui lòng chọn người quản lý dự án hợp lệ!");
                    return;
                }
                string[] parts = managerText.Split('-');
                if (parts.Length < 2)
                {
                    Message.error(this.FindForm(), "Dữ liệu người quản lý không hợp lệ!");
                    return;
                }
                if (!int.TryParse(parts[0].Trim(), out int managerId))
                {
                    Message.error(this.FindForm(), "ID người quản lý không hợp lệ!");
                    return;
                }
                var manager = _employeeRepository.GetAll().FirstOrDefault(emp => emp.EmployeeID == managerId);
                if (manager == null)
                {
                    Message.error(this.FindForm(), "Không tìm thấy người quản lý trong danh sách nhân sự!");
                    return;
                }
                var newProject = new Project
                {
                    ProjectName = projectName,
                    Description = description,
                    StartDate = startDate,
                    EndDate = endDate,
                    Status = status,
                    ManagerBy = managerId,
                    ManagerName = manager.FullName,
                    CreatedBy = SessionManager.CurrentUser.UserID
                };
                if (_isEdit)
                {
                    newProject.ProjectID = _project.ProjectID;
                    _projectRepository.Update(newProject);
                }
                else
                {
                    _projectRepository.Insert(newProject);
                }
                DialogResult = DialogResult.OK;
                Tag = newProject;
                Close();
            }
            catch (Exception ex)
            {
                Message.error(this.FindForm(), "Lỗi khi lưu dự án: " + ex.Message);
            }
        }
        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
        private void cbManager_SelectedValueChanged(object sender, AntdUI.ObjectNEventArgs e)
        {
            if (cbManager.SelectedValue != null)
                cbManager.Text = cbManager.SelectedValue.ToString();
        }
        private void cboStatus_SelectedValueChanged(object sender, AntdUI.ObjectNEventArgs e)
        {
            if (cboStatus.SelectedValue != null)
                cboStatus.Text = cboStatus.SelectedValue.ToString();
        }
        private System.Collections.Generic.IEnumerable<Employee> _employee_repository_getall()
        {
            try
            {
                return _employeeRepository.GetAll();
            }
            catch
            {
                return Enumerable.Empty<Employee>();
            }
        }
    }
}