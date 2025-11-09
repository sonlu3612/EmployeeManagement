using AntdUI;
using EmployeeManagement.DAL.Helpers;
using EmployeeManagement.DAL.Repositories;
using EmployeeManagement.Dialogs;
using EmployeeManagement.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Message = AntdUI.Message;

namespace EmployeeManagement.Pages
{
    public partial class Page_Project : UserControl
    {
        private readonly ProjectRepository _projectRepository = new ProjectRepository();
        private readonly EmployeeRepository employeeRepository = new EmployeeRepository();
        private readonly TaskRepository taskRepository = new TaskRepository();

        // Danh sách gốc để xử lý xóa, sửa, lọc
        private List<Project> visibleProjects = new List<Project>();

        public Page_Project()
        {
            InitializeComponent();
        }

        #region Helpers

        private bool IsInDesignMode() =>
            LicenseManager.UsageMode == LicenseUsageMode.Designtime ||
            (Site != null && Site.DesignMode);

        private bool IsAdmin() =>
            SessionManager.CurrentUser?.Roles?.Contains("Admin") ?? false;

        private bool IsProjectManager() =>
            SessionManager.CurrentUser?.Roles?.Contains("Quản lý dự án") ?? false;

        private bool IsEmployee() =>
            SessionManager.CurrentUser?.Roles?.Contains("Nhân viên") ?? false;

        #endregion Helpers

        #region Display Item (chỉ ngày)

        private object CreateDisplayItem(Project p)
        {
            ProjectStats stats = _projectRepository.GetProjectStats(p.ProjectID);
            int progressValue = stats != null ? (int)stats.CompletionPercentage : 0;
            string progressText = stats != null ? $"{stats.CompletedTasks}/{stats.TotalTasks}" : "0/0";

            //var cellProgress = new AntdUI.CellProgress
            //{
            //    Value = progressValue,
            //    Text = progressText,
            //    BackColor = System.Drawing.Color.Transparent
            //}.SetSize(150, 24);

            var cellProgress = new AntdUI.CellProgress((float)progressValue / 100);

            return new
            {
                p.ProjectName,
                p.Description,
                StartDateDisplay = p.StartDate.ToString("dd/MM/yyyy"),
                EndDateDisplay = p.EndDate?.ToString("dd/MM/yyyy") ?? "N/A",
                p.Status,
                p.CreatedByName,
                p.ManagerName,
                Progress = cellProgress,
                p.ProjectID
            };
        }

        #endregion Display Item (chỉ ngày)

        #region Load Data

        public void LoadData()
        {
            try
            {
                List<Project> projects = new List<Project>();

                if (IsAdmin())
                {
                    projects = _projectRepository.GetAll().ToList();
                }
                else if (IsProjectManager())
                {
                    projects = _projectRepository.GetAll()
                        .Where(p => p.ManagerBy == SessionManager.CurrentUser.UserID)
                        .ToList();
                }
                else if (IsEmployee())
                {
                    var myTasks = taskRepository.GetByEmployee(SessionManager.CurrentUser.UserID);
                    var projectIds = myTasks.Select(t => t.ProjectID).Distinct().ToList();
                    projects = _projectRepository.GetAll()
                        .Where(p => projectIds.Contains(p.ProjectID))
                        .ToList();
                }

                visibleProjects = projects;

                var displayList = projects.Select(p => CreateDisplayItem(p)).ToList();
                tbProject.DataSource = displayList;

                // Load dropdown quản lý
                if (cbQuanLy.Items.Count == 0)
                {
                    loadEmployeesName(cbQuanLy, "ManagerBy");
                }

                // Load dropdown trạng thái
                if (cbTrangThai.Items.Count == 0)
                {
                    cbTrangThai.Items.Add("Lên kế hoạch");
                    cbTrangThai.Items.Add("Đang thực hiện");
                    cbTrangThai.Items.Add("Hoàn thành");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải dự án: " + ex.Message);
            }
        }

        #endregion Load Data

        #region Dropdown Employees

        private void loadEmployeesName(AntdUI.Dropdown dropdown, string filterType)
        {
            var employees = employeeRepository.GetAll();
            dropdown.Items.Clear();
            dropdown.Items.Insert(0, "Tất cả");
            foreach (var emp in employees)
            {
                string displayText = $"{emp.EmployeeID} - {emp.FullName}";
                dropdown.Items.Add(displayText);
            }
        }

        #endregion Dropdown Employees

        #region Get Selected Project

        private Project GetSelectedProject()
        {
            int index = tbProject.SelectedIndex - 1;
            if (index < 0) return null;

            if (tbProject.DataSource is System.Collections.IList list && index < list.Count)
            {
                dynamic row = list[index];
                int projectId = row.ProjectID;
                return visibleProjects.FirstOrDefault(p => p.ProjectID == projectId);
            }
            return null;
        }

        #endregion Get Selected Project

        #region Buttons & Events

        private void btnXoa_Click(object sender, EventArgs e)
        {
            var project = GetSelectedProject();
            if (project == null)
            {
                Message.warn(this.FindForm(), "Vui lòng chọn dự án cần xóa!");
                return;
            }

            if (!IsAdmin() && !(IsProjectManager() && project.ManagerBy == SessionManager.CurrentUser.UserID))
            {
                Message.warn(this.FindForm(), "Bạn không có quyền xóa dự án này!");
                return;
            }

            var modalConfig = Modal.config(
                this.FindForm(),
                "Xác nhận xoá",
                $"Bạn có chắc muốn xoá dự án \"{project.ProjectName}\" không?",
                TType.Warn
            );
            modalConfig.OkText = "Xoá";
            modalConfig.CancelText = "Huỷ";
            modalConfig.OkType = TTypeMini.Success;
            modalConfig.OnOk = _ =>
            {
                try
                {
                    _projectRepository.Delete(project.ProjectID);
                    LoadData();
                    Message.success(this.FindForm(), "Xóa dự án thành công!");
                }
                catch (Exception ex)
                {
                    Message.error(this.FindForm(), "Lỗi khi xoá dự án: " + ex.Message);
                }
                return true;
            };
            Modal.open(modalConfig);
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            var frm = new frmProject();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                var newProject = frm.Tag as Project;
                if (newProject != null)
                {
                    //_projectRepository.Insert(newProject);
                    LoadData();
                    Message.success(this.FindForm(), "Thêm dự án thành công!");
                }
            }
        }

        private void btnSync_Click(object sender, EventArgs e)
        {
            LoadData();
            cbQuanLy.Text = "Quản lý";
            cbTrangThai.Text = "Trạng thái";
        }

        #endregion Buttons & Events

        #region Context Menu

        private void tbProject_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && tbProject.SelectedIndex >= 0)
            {
                ctm1.Show(Cursor.Position);
            }
        }

        private void taskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var project = GetSelectedProject();
            if (project == null)
            {
                Message.error(this.FindForm(), "Không thể lấy dữ liệu dự án được chọn!");
                return;
            }

            var frm = new frmManageTasks();
            frm.frmManageTasks_Load(project.ProjectID);
            frm.ShowDialog();
        }

        private void chỉnhSửaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var project = GetSelectedProject();
            if (project == null)
            {
                Message.error(this.FindForm(), "Vui lòng chọn dự án cần chỉnh sửa!");
                return;
            }

            if (!IsAdmin() && !(IsProjectManager() && project.ManagerBy == SessionManager.CurrentUser.UserID))
            {
                Message.warn(this.FindForm(), "Bạn không có quyền chỉnh sửa dự án này!");
                return;
            }

            var frm = new frmProject(project);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                var edited = frm.Tag as Project;
                if (edited != null)
                {
                    try
                    {
                        _projectRepository.Update(edited);
                        LoadData();
                        Message.success(this.FindForm(), "Sửa thông tin dự án thành công!");
                    }
                    catch (Exception ex)
                    {
                        Message.error(this.FindForm(), "Lỗi khi cập nhật dự án: " + ex.Message);
                    }
                }
            }
        }

        private void cậpNhậtToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var project = GetSelectedProject();
            if (project == null)
            {
                Message.error(this.FindForm(), "Vui lòng chọn dự án cần cập nhật!");
                return;
            }

            if (!IsAdmin() && !IsProjectManager())
            {
                Message.warn(this.FindForm(), "Bạn không có quyền cập nhật dự án này!");
                return;
            }

            var frm = new frmProject(project);
            if (frm.ShowDialog() == DialogResult.OK)
            {
                Message.success(this.FindForm(), "Cập nhật dự án thành công");
                LoadData();
            }
        }

        #endregion Context Menu

        #region Search & Filter

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string q = txtTim.Text?.Trim() ?? "";
                var projects = visibleProjects.AsEnumerable();

                // Tìm theo tên
                if (!string.IsNullOrWhiteSpace(q))
                {
                    projects = projects.Where(p =>
                        !string.IsNullOrEmpty(p.ProjectName) &&
                        p.ProjectName.IndexOf(q, StringComparison.OrdinalIgnoreCase) >= 0);
                }

                // Lọc theo quản lý
                int? managerId = null;
                var selectedManager = cbQuanLy.Text;
                if (!string.IsNullOrWhiteSpace(selectedManager) && !selectedManager.Equals("Tất cả", StringComparison.OrdinalIgnoreCase))
                {
                    var token = selectedManager.Split(new[] { ' ', '-', ':' }, StringSplitOptions.RemoveEmptyEntries)[0];
                    if (int.TryParse(token, out int id)) managerId = id;
                }
                if (managerId.HasValue)
                {
                    projects = projects.Where(p => p.ManagerBy == managerId.Value);
                }

                // Lọc theo trạng thái - KHAI BÁO status TRƯỚC
                string statusFilter = "";
                if (!string.IsNullOrWhiteSpace(cbTrangThai.Text) && cbTrangThai.Text != "Trạng thái")
                {
                    statusFilter = cbTrangThai.Text.Trim();
                    projects = projects.Where(p =>
                        p.Status != null &&
                        p.Status.IndexOf(statusFilter, StringComparison.OrdinalIgnoreCase) >= 0);
                }

                var result = projects.ToList();
                var displayList = result.Select(p => CreateDisplayItem(p)).ToList();
                tbProject.DataSource = displayList;

                if (result.Count == 0)
                    Message.warn(this.FindForm(), "Không tìm thấy kết quả phù hợp.");
                else
                    Message.success(this.FindForm(), $"Tìm thấy {result.Count} dự án.");
            }
            catch (Exception ex)
            {
                Message.error(this.FindForm(), "Lỗi khi tìm kiếm: " + ex.Message);
            }
        }

        private void cbQuanLy_SelectedValueChanged(object sender, ObjectNEventArgs e)
        {
            string selected = e.Value?.ToString() ?? "";
            if (string.IsNullOrWhiteSpace(selected)) return;

            if (selected.Equals("Tất cả", StringComparison.OrdinalIgnoreCase))
            {
                var display = visibleProjects.Select(p => CreateDisplayItem(p)).ToList();
                tbProject.DataSource = display;
                return;
            }
            var parts = selected.Split(new[] { '-' }, 2, StringSplitOptions.RemoveEmptyEntries);

            string fullName = parts[1].Trim();
            cbQuanLy.Text = fullName;

            if (parts.Length == 2 && int.TryParse(parts[0].Trim(), out int managerId))
            {
                var filtered = visibleProjects.Where(p => p.ManagerBy == managerId).ToList();
                var display = filtered.Select(p => CreateDisplayItem(p)).ToList();
                tbProject.DataSource = display;
                Message.success(this.FindForm(), $"Tìm thấy {filtered.Count} dự án.");
            }
            else
            {
                Message.warn(this.FindForm(), "Không nhận diện được quản lý được chọn.");
            }
        }

        private void cbTrangThai_SelectedValueChanged(object sender, ObjectNEventArgs e)
        {
            cbTrangThai.Text = cbTrangThai.SelectedValue.ToString();
            string status = e.Value?.ToString() ?? "";
            var filtered = string.IsNullOrEmpty(status)
                ? visibleProjects
                : visibleProjects.Where(p =>
                    p.Status != null &&
                    p.Status.IndexOf(status, StringComparison.OrdinalIgnoreCase) >= 0).ToList();

            var display = filtered.Select(p => CreateDisplayItem(p)).ToList();
            tbProject.DataSource = display;
        }

        #endregion Search & Filter

        #region Load Form

        private void Page_Project_Load(object sender, EventArgs e)
        {
            if (IsInDesignMode()) return;

            tbProject.Columns.Clear();
            tbProject.Columns.Add(new Column("ProjectName", "Tên dự án"));
            tbProject.Columns.Add(new Column("Description", "Chi tiết"));
            tbProject.Columns.Add(new Column("StartDateDisplay", "Ngày bắt đầu"));
            tbProject.Columns.Add(new Column("EndDateDisplay", "Ngày hết hạn"));
            tbProject.Columns.Add(new Column("Status", "Trạng thái"));
            tbProject.Columns.Add(new Column("CreatedByName", "Người tạo"));
            tbProject.Columns.Add(new Column("ManagerName", "Người quản lý"));
            tbProject.Columns.Add(new Column("Progress", "Tiến độ"));

            LoadData();

            if (!IsAdmin())
                btnThem.Enabled = false;

            if (!IsAdmin() && !IsProjectManager())
                btnXoa.Enabled = false;
        }

        #endregion Load Form

        private void documentsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var project = GetSelectedProject();
            if (project == null)
            {
                Message.error(this.FindForm(), "Không thể lấy dữ liệu dự án được chọn!");
                return;
            }

            // Phân quyền: Admin hoặc Quản lý dự án của dự án này
            if (!IsAdmin() && !(IsProjectManager() && project.ManagerBy == SessionManager.CurrentUser.UserID))
            {
                Message.warn(this.FindForm(), "Bạn không có quyền xem tài liệu cho dự án này!");
                return;
            }

            var frm = new frmProjectFile(project.ProjectID);
            Console.WriteLine(project.ProjectID);
            frm.ShowDialog();
        }

        private Table.CellStyleInfo tbProject_SetRowStyle(object sender, TableSetRowStyleEventArgs e)
        {
            if (e.Index % 2 == 0)
            {
                return new AntdUI.Table.CellStyleInfo
                {
                    BackColor = Color.FromArgb(208, 231, 252)
                };
            }
            return null;
        }
    }
}