using EmployeeManagement.DAL.Helpers;
using EmployeeManagement.DAL.Interfaces;
using EmployeeManagement.DAL.Repositories;
using EmployeeManagement.Models;
using System;
using System.ComponentModel;
using System.Net;
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
                cbManager.Enabled = false ;
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
                cbManager.Text = _project.ManagerBy.ToString() + " - "+_project.ManagerName;
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
            var employees = _employeeRepository.GetAll();
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
                if (cbManager.SelectedValue == null)
                {
                    MessageBox.Show("Vui lòng chọn người quản lý dự án!");
                    return;
                }

                // Lấy giá trị ID và tên từ cbManager.Text (dạng: "1 - Nguyễn Văn A")
                string selectedText = cbManager.Text.Trim();
                string[] parts = selectedText.Split('-');
                if (parts.Length < 2)
                {
                    MessageBox.Show("Dữ liệu người quản lý không hợp lệ!");
                    return;
                }

                int managerId = int.Parse(parts[0].Trim());
                string managerName = parts[1].Trim();

                var newProject = new Project
                {
                    ProjectName = txtProjectName.Text.Trim(),
                    Description = txtDescription.Text.Trim(),
                    StartDate = (DateTime)dtStartDate.Value,
                    EndDate = dtEndDate.Value,
                    Status = cboStatus.Text,
                    ManagerBy = managerId,
                    ManagerName = managerName,
                    CreatedBy = SessionManager.CurrentUser.UserID
                };

                if (_isEdit)
                {
                 
                    newProject.ProjectID = _project.ProjectID;
                    _projectRepository.Update(newProject);
                    //Message.success(this.FindForm(), "Cập nhật dự án thành công!");
                }
                else
                {
                    Console.WriteLine("Tạo dự án mới");
                    _projectRepository.Insert(newProject);
                    //Message.success(this.FindForm(), "Thêm dự án thành công!");
                }

                DialogResult = DialogResult.OK;
                Tag = newProject;
                Close();
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu dự án: " + ex.Message);
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void cbManager_SelectedValueChanged(object sender, AntdUI.ObjectNEventArgs e)
        {
            cbManager.Text = cbManager.SelectedValue.ToString();
        }

        private void cboStatus_SelectedValueChanged(object sender, AntdUI.ObjectNEventArgs e)
        {
            cboStatus.Text = cboStatus.SelectedValue.ToString();
        }
    }
}