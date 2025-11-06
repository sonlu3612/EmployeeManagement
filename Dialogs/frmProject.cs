using EmployeeManagement.DAL.Helpers;
using EmployeeManagement.DAL.Interfaces;
using EmployeeManagement.DAL.Repositories;
using EmployeeManagement.Models;
using System;
using System.Windows.Forms;

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
        private void frmProject_Load(object sender, EventArgs e)
        {
            loadEmployeesName();
            if (_isEdit)
            {
                txtProjectName.Text = _project.ProjectName;
                txtDescription.Text = _project.Description;
                dtStartDate.Value = _project.StartDate;
                dtEndDate.Value = _project.EndDate;
                cbManager.SelectedValue = _project.CreatedBy;
                cbManager.SelectedValue = _project.ManagerBy;
            }
            else
            {
                txtProjectName.Clear();
                txtDescription.Clear();
                dtStartDate.Value = DateTime.Now;
                dtEndDate.Value = DateTime.Now;
                cbManager.SelectedValue = currentUser.UserID;
                cboStatus.Text = "Planning";
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
                    Status = "Planning",
                    ManagerBy = managerId,
                    ManagerName = managerName,
                    CreatedBy = SessionManager.CurrentUser.UserID
                };

                if (_isEdit)
                {
                    newProject.ProjectID = _project.ProjectID;
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
    }
}