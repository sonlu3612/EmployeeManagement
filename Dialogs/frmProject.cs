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

        public frmProject(Project project = null)
        {
            InitializeComponent();
            _project = project;
            _isEdit = project != null;
        }

        private void frmProject_Load(object sender, EventArgs e)
        {
            if (_isEdit)
            {
                txtProjectName.Text = _project.ProjectName;
                txtDescription.Text = _project.Description;
                dtStartDate.Value = _project.StartDate;
                dtEndDate.Value = _project.EndDate;
            }
            else
            {
                txtProjectName.Clear();
                txtDescription.Clear();
                dtStartDate.Value = DateTime.Now;
                dtEndDate.Value = DateTime.Now;
                cbCreate.Text = currentUser.UserID.ToString();
                cboStatus.Text = "Planning";
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            var newProject = new Project
            {
                ProjectName = txtProjectName.Text.Trim(),
                Description = txtDescription.Text.Trim(),
                StartDate = (DateTime)dtStartDate.Value,
                EndDate = dtEndDate.Value,
                Status = "Planning",
                CreatedBy = int.Parse(cbCreate.Text)

            };

            if (_isEdit)
            {
                newProject.ProjectID = _project.ProjectID; 
                DialogResult = DialogResult.OK;
                Tag = newProject;
            }
            else
            {
                DialogResult = DialogResult.OK;
                Tag = newProject;
            }

            Close();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}
