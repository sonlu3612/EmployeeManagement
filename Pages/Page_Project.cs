using AntdUI;
using EmployeeManagement.DAL.Helpers;
using EmployeeManagement.DAL.Interfaces;
using EmployeeManagement.DAL.Repositories;
using EmployeeManagement.Dialogs;
using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Message = AntdUI.Message;
namespace EmployeeManagement.Pages
{
    public partial class Page_Project : UserControl
    {
        private readonly ProjectRepository _projectRepository = new ProjectRepository();
        private readonly EmployeeRepository employeeRepository = new EmployeeRepository();
        private readonly TaskRepository taskRepository = new TaskRepository();
        private List<Project> visibleProjects;
        public Page_Project()
        {
            InitializeComponent();
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
        private void LoadData()
        {
            if (IsAdmin())
            {
                visibleProjects = _projectRepository.GetAll().ToList();
            }
            else if (IsProjectManager())
            {
                visibleProjects = _projectRepository.GetAll()
                    .Where(p => p.ManagerBy == SessionManager.CurrentUser.UserID)
                    .ToList();
            }
            else if (IsEmployee())
            {
                var myTasks = taskRepository.GetByEmployee(SessionManager.CurrentUser.UserID);
                var projectIds = myTasks.Select(t => t.ProjectID).Distinct().ToList();
                visibleProjects = _projectRepository.GetAll()
                    .Where(p => projectIds.Contains(p.ProjectID))
                    .ToList();
            }
            else
            {
                visibleProjects = new List<Project>();
            }
            tbProject.DataSource = visibleProjects;
            if (cbQuanLy.Items.Count == 0)
            {
                loadEmployeesName(cbQuanLy, "ManagerBy");
            }
            if (cbTrangThai.Items.Count == 0)
            {
                cbTrangThai.Items.AddRange(visibleProjects.Select(p => p.Status).Distinct().ToArray());
            }
        }
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
        private void btnXoa_Click(object sender, EventArgs e)
        {
            var selectedIndex = tbProject.SelectedIndex - 1;
            if (selectedIndex < 0)
            {
                Message.warn(this.FindForm(), "Vui lòng chọn dự án cần xóa!");
                return;
            }
            if (tbProject.DataSource is IList<Project> projects && selectedIndex < projects.Count)
            {
                var record = projects[selectedIndex];
                if (record == null)
                {
                    Message.error(this.FindForm(), "Không thể lấy dữ liệu dự án được chọn!");
                    return;
                }
                if (!IsAdmin() && !(IsProjectManager() && record.ManagerBy == SessionManager.CurrentUser.UserID))
                {
                    Message.warn(this.FindForm(), "Bạn không có quyền xóa dự án này!");
                    return;
                }
                var modalConfig = Modal.config(
                    this.FindForm(),
                    "Xác nhận xoá",
                    $"Bạn có chắc muốn xoá dự án \"{record.ProjectName}\" không?",
                    TType.Warn
                );
                modalConfig.OkText = "Xoá";
                modalConfig.CancelText = "Huỷ";
                modalConfig.OkType = TTypeMini.Success;
                modalConfig.OnOk = (cfg) =>
                {
                    try
                    {
                        _projectRepository.Delete(record.ProjectID);
                        LoadData();
                        Message.success(this.FindForm(), "Xóa dự án thành công!");
                    }
                    catch (Exception ex)
                    {
                        Message.error(this.FindForm(), "Lỗi khi xoá dự án: " + ex.Message);
                    }
                    return true; // Đóng modal
                };
                Modal.open(modalConfig);
            }
            else
            {
                Message.error(this.FindForm(), "Không thể lấy dữ liệu dự án được chọn!");
            }
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            frmProject frm = new frmProject();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                var newProject = frm.Tag as Project;
                if (newProject != null)
                {
                    _projectRepository.Insert(newProject);
                    LoadData();
                    Message.success(this.FindForm(), "Thêm dự án thành công!");
                }
            }
        }
        private void tbProject_CellClick(object sender, TableClickEventArgs e)
        {
        }
        private void tbProject_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int sel = tbProject.SelectedIndex;
                if (sel >= 0)
                {
                    ctm1.Show(Cursor.Position);
                }
            }
        }
        private void taskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int selectedIndex = tbProject.SelectedIndex - 1;
            if (tbProject.DataSource is IList<Project> projects && selectedIndex >= 0 && selectedIndex < projects.Count)
            {
                var record = projects[selectedIndex];
                if (record == null) { Message.error(this.FindForm(), "Không thể lấy dữ liệu dự án được chọn!"); return; }
                int id = record.ProjectID;
                frmManageTasks frm = new frmManageTasks();
                frm.frmManageTasks_Load(id);
                frm.ShowDialog();
            }
            else
            {
                Message.error(this.FindForm(), "Không thể lấy dữ liệu dự án được chọn!");
            }
        }
        private void chỉnhSửaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int selectedIndex = tbProject.SelectedIndex - 1;
            if (tbProject.DataSource is IList<Project> projects && selectedIndex >= 0 && selectedIndex < projects.Count)
            {
                var record = projects[selectedIndex];
                if (record == null)
                {
                    Message.error(this.FindForm(), "Không thể lấy dữ liệu dự án được chọn!");
                    return;
                }
                if (!IsAdmin() && !(IsProjectManager() && record.ManagerBy == SessionManager.CurrentUser.UserID))
                {
                    Message.warn(this.FindForm(), "Bạn không có quyền chỉnh sửa dự án này!");
                    return;
                }
                frmProject frm = new frmProject(record);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    var editedProject = frm.Tag as Project;
                    if (editedProject != null)
                    {
                        try
                        {
                            _projectRepository.Update(editedProject);
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
            else
            {
                Message.error(this.FindForm(), "Vui lòng chọn dự án cần chỉnh sửa!");
            }
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string q = txtTim.Text?.Trim() ?? "";
                var projects = visibleProjects.AsEnumerable();
                if (!string.IsNullOrWhiteSpace(q))
                {
                    projects = projects.Where(p =>
                        !string.IsNullOrEmpty(p.ProjectName) &&
                        p.ProjectName.IndexOf(q, StringComparison.OrdinalIgnoreCase) >= 0);
                }
                int? employeeIdFilter = null;
                var selectedValue = cbQuanLy.Text;
                if (!string.IsNullOrWhiteSpace(selectedValue) && !selectedValue.Equals("Tất cả", StringComparison.OrdinalIgnoreCase))
                {
                    var token = selectedValue.Split(new[] { ' ', '-', ':' }, StringSplitOptions.RemoveEmptyEntries)[0];
                    if (int.TryParse(token, out int empId))
                    {
                        employeeIdFilter = empId;
                    }
                }
                if (employeeIdFilter.HasValue)
                {
                    projects = projects.Where(p => p.ManagerBy == employeeIdFilter.Value);
                }
                if (!string.IsNullOrWhiteSpace(cbTrangThai.Text) && cbTrangThai.Text != "Trạng thái")
                {
                    string statusFilter = cbTrangThai.Text.Trim();
                    projects = projects.Where(p => !string.IsNullOrEmpty(p.Status) &&
                                                   p.Status.IndexOf(statusFilter, StringComparison.OrdinalIgnoreCase) >= 0);
                }
                var result = projects.ToList();
                tbProject.DataSource = result;
                if (result.Count == 0)
                {
                    Message.warn(this.FindForm(), "Không tìm thấy kết quả phù hợp.");
                }
                else
                {
                    Message.success(this.FindForm(), $"Tìm thấy {result.Count} dự án.");
                }
            }
            catch (Exception ex)
            {
                Message.error(this.FindForm(), "Lỗi khi tìm kiếm: " + ex.Message);
            }
        }
        private void cbNhanVien_SelectedValueChanged(object sender, ObjectNEventArgs e)
        {
            try
            {
                var selectedValue = e.Value?.ToString();
                if (string.IsNullOrWhiteSpace(selectedValue))
                    return;
                cbQuanLy.Text = selectedValue;
                if (selectedValue.Equals("Tất cả", StringComparison.OrdinalIgnoreCase))
                {
                    tbProject.DataSource = visibleProjects;
                    return;
                }
                var token = selectedValue.Split(new[] { ' ', '-', ':' }, StringSplitOptions.RemoveEmptyEntries)[0];
                if (int.TryParse(token, out int employeeId))
                {
                    var projects = visibleProjects
                        .Where(p => p.ManagerBy == employeeId)
                        .ToList();
                    tbProject.DataSource = projects;
                    Message.success(this.FindForm(), $"Tìm thấy {projects.Count} dự án.");
                }
                else
                {
                    Message.warn(this.FindForm(), $"Không tìm thấy.");
                }
            }
            catch (Exception ex)
            {
                Message.error(this.FindForm(), "Lỗi khi lọc theo nhân viên: " + ex.Message);
            }
        }
        private void cbQuanLy_SelectedValueChanged(object sender, ObjectNEventArgs e)
        {
            try
            {
                var selectedValue = e.Value?.ToString();
                if (string.IsNullOrWhiteSpace(selectedValue))
                    return;
                cbQuanLy.Text = selectedValue;
                if (selectedValue.Equals("Tất cả", StringComparison.OrdinalIgnoreCase))
                {
                    tbProject.DataSource = visibleProjects;
                    return;
                }
                var token = selectedValue.Split(new[] { ' ', '-', ':' }, StringSplitOptions.RemoveEmptyEntries)[0];
                if (int.TryParse(token, out int managerId))
                {
                    var projects = visibleProjects
                        .Where(p => p.ManagerBy == managerId)
                        .ToList();
                    tbProject.DataSource = projects;
                    Message.success(this.FindForm(), $"Tìm thấy {projects.Count} dự án.");
                }
                else
                {
                    Message.warn(this.FindForm(), $"Không tìm thấy.");
                }
            }
            catch (Exception ex)
            {
                Message.error(this.FindForm(), "Lỗi khi lọc theo quản lý: " + ex.Message);
            }
        }
        private void btnSync_Click(object sender, EventArgs e)
        {
            LoadData();
            cbQuanLy.Text = "Quản lý";
            cbTrangThai.Text = "Trạng thái";
        }
        private void cbTrangThai_SelectedValueChanged(object sender, ObjectNEventArgs e)
        {
            cbTrangThai.Text = e.Value?.ToString() ?? "";
            var filteredProjects = visibleProjects
                .Where(p => p.Status.Contains(cbTrangThai.Text))
                .ToList();
            tbProject.DataSource = filteredProjects;
        }
        private void Page_Project_Load(object sender, EventArgs e)
        {
            if (IsInDesignMode()) return;
            tbProject.Columns.Add(new Column("ProjectName", "Tên dự án"));
            tbProject.Columns.Add(new Column("Description", "Chi tiết"));
            tbProject.Columns.Add(new Column("StartDate", "Ngày bắt đầu"));
            tbProject.Columns.Add(new Column("EndDate", "Ngày hết hạn"));
            tbProject.Columns.Add(new Column("Status", "Trạng thái"));
            tbProject.Columns.Add(new Column("CreatedByName", "Người tạo"));
            tbProject.Columns.Add(new Column("ManagerName", "Người quản lý"));
            LoadData();
            if (!IsAdmin())
            {
                btnThem.Enabled = false;
            }
            if (!IsAdmin() && !IsProjectManager())
            {
                btnXoa.Enabled = false;
            }
        }
    }
}