using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EmployeeManagement.DAL.Repositories;
using EmployeeManagement.Pages;
namespace EmployeeManagement.Dialogs
{
    public partial class frmProjectFile : Form
    {
        private int _projectId;
        private Page_ProjectFile ucProjectFile;
        public frmProjectFile(int projectID)
        {
            InitializeComponent();
            _projectId = projectID;
            var repo = new ProjectRepository();
            var project = repo.GetById(_projectId);
            string projectName = project?.ProjectName ?? "Unknown Project";
            page_ProjectFile1.LoadProjectFiles(_projectId, projectName);
        }
    }
}