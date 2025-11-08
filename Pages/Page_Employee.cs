using AntdUI;
using EmployeeManagement.DAL.Helpers;
using EmployeeManagement.DAL.Repositories;
using EmployeeManagement.Dialogs;
using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using Label = AntdUI.Label;
using Message = AntdUI.Message;
namespace EmployeeManagement.Pages
{
    public partial class Page_Employee : UserControl
    {
        private EmployeeRepository employeeRepository = new EmployeeRepository();
        private DepartmentRepository departmentRepository = new DepartmentRepository();
        private List<Employee> visibleEmployees;
        public Page_Employee()
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
        private bool IsDepartmentManager()
        {
            return SessionManager.CurrentUser?.Roles?.Contains("Quản lý phòng ban") ?? false;
        }
        private bool IsEmployee()
        {
            return SessionManager.CurrentUser?.Roles?.Contains("Nhân viên") ?? false;
        }
        private void Page_Employee_Load(object sender, EventArgs e)
        {
            if (IsInDesignMode()) return;
            if (tbNV.Columns.Count == 0)
            {
                var avatarCol = new AntdUI.ColumnSelect("EmployeeID", "Ảnh");
                avatarCol.CellType = AntdUI.SelectCellType.Icon;
                avatarCol.SetAlign(AntdUI.ColumnAlign.Center);
                //avatarCol.SetWidth("80");
                tbNV.Columns.Add(avatarCol);
                tbNV.Columns.Add(new AntdUI.Column("FullName", "Họ và tên"));
                tbNV.Columns.Add(new AntdUI.Column("Gender", "Giới tính"));
                tbNV.Columns.Add(new AntdUI.Column("Email", "Email"));
                tbNV.Columns.Add(new AntdUI.Column("Phone", "SĐT"));
                tbNV.Columns.Add(new AntdUI.Column("ProjectSummary", "Dự án"));
                tbNV.Columns.Add(new AntdUI.Column("TaskSummary", "Nhiệm vụ"));
            }
            tbNV.RowHeight = 100;
            tbNV.RowHeightHeader = 30;
            LoadData();
            if (!IsAdmin() && !IsDepartmentManager())
            {
                btnDelete.Enabled = false;
            }
        }
        public void LoadData()
        {
            try
            {
                string projectRoot = Path.GetFullPath(Path.Combine(Application.StartupPath, @"..\..\"));
                var allEmployees = employeeRepository.GetForGrid();
                if (IsAdmin())
                {
                    visibleEmployees = allEmployees.ToList();
                }
                else if (IsDepartmentManager())
                {
                    var myDepartmentIds = departmentRepository.GetAll()
                        .Where(d => d.ManagerID == SessionManager.CurrentUser.UserID)
                        .Select(d => d.DepartmentID)
                        .ToList();

                    visibleEmployees = allEmployees
                        .Where(e => myDepartmentIds.Contains(e.DepartmentID ?? 0))
                        .ToList();

                    var manager = allEmployees.FirstOrDefault(e => e.EmployeeID == SessionManager.CurrentUser.UserID);
                    if (manager != null && !visibleEmployees.Any(e => e.EmployeeID == manager.EmployeeID))
                    {
                        visibleEmployees.Add(manager);
                    }
                }

                else
                {
                    visibleEmployees = allEmployees
                        .Where(e => e.EmployeeID == SessionManager.CurrentUser.UserID)
                        .ToList();
                }
                tbNV.DataSource = visibleEmployees;
                var avatarCol = tbNV.Columns.FirstOrDefault(c => c.Key == "EmployeeID") as AntdUI.ColumnSelect;
                if (avatarCol != null)
                {
                    var items = new List<AntdUI.SelectItem>();
                    foreach (var emp in visibleEmployees)
                    {
                        Image icon = null;
                        try
                        {
                            if (!string.IsNullOrEmpty(emp.AvatarPath))
                            {
                                string fullPath = Path.Combine(projectRoot, emp.AvatarPath);
                                if (File.Exists(fullPath))
                                {
                                    icon = Image.FromFile(fullPath);
                                }
                                else
                                {
                                    Console.WriteLine($"File không tồn tại: {fullPath}");
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Lỗi load avatar cho {emp.FullName}: {ex.Message}");
                        }
                        items.Add(new AntdUI.SelectItem(0, icon, emp.FullName ?? "", emp.EmployeeID));
                    }
                    avatarCol.Items = items;
                }
                if (ddownGender.Items.Count == 0)
                {
                    ddownGender.Items.Add("Tất cả");
                    ddownGender.Items.Add("Nam");
                    ddownGender.Items.Add("Nữ");
                }
                ddownGender.Text = "Giới tính";
            }
            catch (Exception ex)
            {
                Message.error(this.FindForm(), "Lỗi khi load dữ liệu: " + ex.Message);
            }
        }
        //private void btnAdd_Click(object sender, EventArgs e)
        //{
        // frmEmployee frm = new frmEmployee();
        // if (frm.ShowDialog() == DialogResult.OK)
        // {
        // var newEmployee = frm.Tag as Employee;
        // if (newEmployee != null)
        // {
        // employeeRepository.Insert(newEmployee);
        // LoadData(); // 🔁 Reload để cập nhật avatar mới
        // Message.success(this.FindForm(), "Thêm nhân viên thành công!");
        // }
        // }
        //}
        private void btnDelete_Click(object sender, EventArgs e)
        {
            var selectedIndex = tbNV.SelectedIndex - 1;
            if (selectedIndex < 0)
            {
                Message.warn(this.FindForm(), "Vui lòng chọn nhân viên cần xóa!");
                return;
            }
            if (tbNV.DataSource is IList<Employee> employees && selectedIndex < employees.Count)
            {
                var record = employees[selectedIndex];
                if (record == null)
                {
                    Message.error(this.FindForm(), "Không thể lấy dữ liệu nhân viên được chọn!");
                    return;
                }
                if (!IsAdmin())
                {
                    if (!IsDepartmentManager())
                    {
                        btnDelete.Enabled = false;
                        //Message.warn(this.FindForm(), "Bạn không có quyền xóa nhân viên!");
                        return;
                    }
                    var myDepartmentIds = departmentRepository.GetAll()
                        .Where(d => d.ManagerID == SessionManager.CurrentUser.UserID)
                        .Select(d => d.DepartmentID)
                        .ToList();
                    if (!myDepartmentIds.Contains(record.DepartmentID ?? 0))
                    {
                        Message.warn(this.FindForm(), "Bạn không có quyền xóa nhân viên này!");
                        return;
                    }
                }
                var modalConfig = Modal.config(
                    this.FindForm(),
                    "Xác nhận xoá",
                    $"Bạn có chắc muốn xoá nhân viên \"{record.FullName}\" không?",
                    TType.Warn
                );
                modalConfig.OkText = "Xoá";
                modalConfig.CancelText = "Huỷ";
                modalConfig.OkType = TTypeMini.Success;
                modalConfig.OnOk = (cfg) =>
                {
                    try
                    {
                        employeeRepository.Delete(record.EmployeeID);
                        LoadData(); // 🔁 Reload sau khi xóa
                        Message.success(this.FindForm(), "Xóa nhân viên thành công!");
                    }
                    catch (Exception ex)
                    {
                        Message.error(this.FindForm(), "Lỗi khi xoá nhân viên: " + ex.Message);
                    }
                    return true;
                };
                Modal.open(modalConfig);
            }
            else
            {
                Message.error(this.FindForm(), "Không thể lấy dữ liệu nhân viên được chọn!");
            }
        }
        private void btnSync_Click(object sender, EventArgs e)
        {
            txtTim.Text = string.Empty;
            ddownGender.Text = "Giới tính";
            LoadData();
        }
        private void ddownGender_SelectedValueChanged(object sender, ObjectNEventArgs e)
        {
            ddownGender.Text = e.Value?.ToString() ?? "";
            if (ddownGender.Text == "Tất cả" || string.IsNullOrEmpty(ddownGender.Text))
            {
                tbNV.DataSource = visibleEmployees;
                return;
            }
            var filtered = visibleEmployees
                .Where(p => p.Gender.ToString().Contains(ddownGender.Text))
                .ToList();
            tbNV.DataSource = filtered;
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string q = txtTim.Text?.Trim() ?? "";
                var employees = visibleEmployees.AsEnumerable();
                if (!string.IsNullOrWhiteSpace(q))
                {
                    employees = employees.Where(p =>
                        !string.IsNullOrEmpty(p.FullName) &&
                        p.FullName.IndexOf(q, StringComparison.OrdinalIgnoreCase) >= 0);
                }
                if (!string.IsNullOrWhiteSpace(ddownGender.Text) && ddownGender.Text != "Giới tính")
                {
                    string empFilter = ddownGender.Text.Trim();
                    employees = employees.Where(p => p.Gender.ToString().Contains(empFilter));
                }
                var result = employees.ToList();
                tbNV.DataSource = result;
                if (result.Count == 0)
                    Message.warn(this.FindForm(), "Không tìm thấy kết quả phù hợp.");
                else
                    Message.success(this.FindForm(), $"Tìm thấy {result.Count} nhân viên.");
            }
            catch (Exception ex)
            {
                Message.error(this.FindForm(), "Lỗi khi tìm kiếm: " + ex.Message);
            }
        }
        private void tbNV_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int sel = tbNV.SelectedIndex;
                if (sel >= 0)
                {
                    ctm1.Show(Cursor.Position);
                }
            }
        }
        private void taskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int selectedIndex = tbNV.SelectedIndex - 1;
            if (tbNV.DataSource is IList<Employee> employees && selectedIndex >= 0 && selectedIndex < employees.Count)
            {
                var record = employees[selectedIndex];
                if (record == null)
                {
                    Message.error(this.FindForm(), "Không thể lấy dữ liệu nhân viên được chọn!");
                    return;
                }
                if (!IsAdmin())
                {
                    if (!IsDepartmentManager())
                    {
                        Message.warn(this.FindForm(), "Bạn không có quyền giao nhiệm vụ cho nhân viên!");
                        return;
                    }
                    var myDepartmentIds = departmentRepository.GetAll()
                        .Where(d => d.ManagerID == SessionManager.CurrentUser.UserID)
                        .Select(d => d.DepartmentID)
                        .ToList();
                    if (!myDepartmentIds.Contains(record.DepartmentID ?? 0))
                    {
                        Message.warn(this.FindForm(), "Bạn không có quyền giao nhiệm vụ cho nhân viên này!");
                        return;
                    }
                }
                int id = record.EmployeeID;
                frmAssignTask frm = new frmAssignTask();
                frm.frmAssignTask_Load(id);
                frm.ShowDialog();
            }
            else
            {
                Message.error(this.FindForm(), "Không thể lấy dữ liệu nhân viên được chọn!");
            }
        }
    }
}