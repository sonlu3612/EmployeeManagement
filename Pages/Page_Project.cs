using AntdUI;
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
        public Page_Project()
        {
            InitializeComponent();
        }
        private bool IsInDesignMode()
        {
            return LicenseManager.UsageMode == LicenseUsageMode.Designtime
                   || (this.Site != null && this.Site.DesignMode);
        }
        private void LoadData()
        {
            tbProject.DataSource = _projectRepository.GetAll().ToList();
            if (cbQuanLy.Items.Count == 0)
            {
                loadEmployeesName(cbQuanLy, "ManagerBy");
            }
            if (cbTrangThai.Items.Count == 0)
            {
                cbTrangThai.Items.AddRange(_projectRepository.GetAll().Select(p => p.Status).Distinct().ToArray());
            }
        }
        private EmployeeRepository employeeRepository = new EmployeeRepository();
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
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string q = txtTim.Text?.Trim() ?? "";
                var projects = _projectRepository.GetAll().AsEnumerable();
                if (!string.IsNullOrWhiteSpace(q))
                {
                    projects = projects.Where(p =>
                        !string.IsNullOrEmpty(p.ProjectName) &&
                        p.ProjectName.IndexOf(q, StringComparison.OrdinalIgnoreCase) >= 0);
                }
                int? employeeIdFilter = null;
                if (cbQuanLy.SelectedValue != null && cbQuanLy.SelectedValue is int empId && empId != 0)
                {
                    employeeIdFilter = empId;
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
                    LoadData();
                    return;
                }
                var token = selectedValue.Split(new[] { ' ', '-', ':' }, StringSplitOptions.RemoveEmptyEntries)[0];
                if (int.TryParse(token, out int employeeId))
                {
                    var projects = _projectRepository.GetAll()
                        .Where(p => p.CreatedBy == employeeId)
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
                    LoadData();
                    return;
                }
                var token = selectedValue.Split(new[] { ' ', '-', ':' }, StringSplitOptions.RemoveEmptyEntries)[0];
                if (int.TryParse(token, out int managerId))
                {
                    var projects = _projectRepository.GetAll()
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
            var filteredProjects = _projectRepository.GetAll()
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
        }
    }
}