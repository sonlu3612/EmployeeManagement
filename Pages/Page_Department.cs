using AntdUI;
using EmployeeManagement.DAL.Interfaces;
using EmployeeManagement.DAL.Repositories;
using EmployeeManagement.Dialogs;
using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EmployeeManagement.DAL.Helpers;
using Message = AntdUI.Message;

namespace EmployeeManagement.Pages
{
    public partial class Page_Department : UserControl
    {
        private DepartmentRepository repository = new DepartmentRepository();
        private EmployeeRepository employeeRepository = new EmployeeRepository();

        public Page_Department()
        {
            InitializeComponent();
        }

        private void ddownEmployee_SelectedValueChanged(object sender, AntdUI.ObjectNEventArgs e)
        {
            try
            {
                string selected = e.Value?.ToString() ?? "";

                if (string.IsNullOrWhiteSpace(selected) || selected.Equals("Tất cả", StringComparison.OrdinalIgnoreCase))
                {
                    ddownTP.Text = "Tất cả";
                    tbPB.DataSource = repository.GetAllWithEmployeeCount();
                    return;
                }

                var parts = selected.Split(new[] { '-' }, 2, StringSplitOptions.RemoveEmptyEntries);
                if (parts.Length == 2 && int.TryParse(parts[0].Trim(), out int employeeId))
                {
                    string fullName = parts[1].Trim();

                    ddownTP.Text = fullName;

                    var filteredDep = repository.GetAllWithEmployeeCount()
                        .Where(d => d.ManagerName == fullName)
                        .ToList();

                    tbPB.DataSource = filteredDep;
                }
                else
                {
                    Message.warn(this.FindForm(), "Không nhận diện được nhân viên được chọn.");
                }
            }
            catch (Exception ex)
            {
                Message.error(this.FindForm(), "Lỗi khi lọc theo trưởng phòng: " + ex.Message);
            }
        }


        private bool IsInDesignMode()
        {
            return LicenseManager.UsageMode == LicenseUsageMode.Designtime
                   || (this.Site != null && this.Site.DesignMode);
        }

        private void Page_Department_Load(object sender, EventArgs e)
        {
            if (IsInDesignMode()) return;

            tbPB.Columns.Add(new Column("DepartmentName", "Tên phòng ban"));
            tbPB.Columns.Add(new Column("Description", "Mô tả"));
            tbPB.Columns.Add(new Column("ManagerName", "Trưởng phòng"));
            tbPB.Columns.Add(new Column("EmployeeCount", "Số nhân viên"));

            LoadData();

            if (IsEmployee())
            {
                btnAdd.Enabled = false;
                btnDelete.Enabled = false;
                chỉnhSửaToolStripMenuItem.Enabled = false;
            }

            if (IsEmployee())
            {
                btnAdd.Enabled = false;
                btnDelete.Enabled = false;
                chỉnhSửaToolStripMenuItem.Enabled = false;
            }
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

        private void LoadData()
        {
            List<Department> departments;

            if (IsAdmin())
            {
                departments = repository.GetAllWithEmployeeCount();
            }
            else if (IsDepartmentManager())
            {
                departments = repository.GetAllWithEmployeeCount()
                    .Where(d => d.ManagerID == SessionManager.CurrentUser.UserID)
                    .ToList();
            }
            else if (IsEmployee())
            {
                var currentEmployee = employeeRepository.GetById(SessionManager.CurrentUser.UserID);
                if (currentEmployee?.DepartmentID != null)
                {
                    departments = repository.GetAllWithEmployeeCount()
                        .Where(d => d.DepartmentID == currentEmployee.DepartmentID)
                        .ToList();
                }
                else
                {
                    departments = new List<Department>();
                }
            }
            else
            {
                departments = new List<Department>();
            }

            tbPB.DataSource = departments;

            if (ddownTP.Items.Count == 0)
            {
                var employees = employeeRepository.GetAll();
                ddownTP.Items.Clear();
                ddownTP.Items.Add("Tất cả");
                foreach (var em in employees)
                {
                    string display = $"{em.EmployeeID} - {em.FullName}";
                    ddownTP.Items.Add(display);
                }
            }
        }


        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (!IsAdmin()) return; // Extra check, though button is disabled

            frmDepartment frm = new frmDepartment();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                var newDepartment = frm.Tag as Department;
                if (newDepartment != null)
                {
                    repository.Insert(newDepartment);
                    LoadData();
                    Message.success(this.FindForm(), "Thêm phòng ban thành công!");
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (!IsAdmin()) return; // Extra check

            var selectedIndex = tbPB.SelectedIndex - 1;
            if (selectedIndex < 0)
            {
                Message.warn(this.FindForm(), "Vui lòng chọn phòng ban cần xóa!");
                return;
            }

            if (tbPB.DataSource is IList<Department> departments && selectedIndex < departments.Count)
            {
                var record = departments[selectedIndex];
                if (record == null)
                {
                    Message.error(this.FindForm(), "Không thể lấy dữ liệu phòng ban được chọn!");
                    return;
                }

                var modalConfig = Modal.config(
                    this.FindForm(),
                    "Xác nhận xoá",
                    $"Bạn có chắc muốn xoá phòng ban \"{record.DepartmentName}\" không?",
                    TType.Warn
                );
                modalConfig.OkText = "Xoá";
                modalConfig.CancelText = "Huỷ";
                modalConfig.OkType = TTypeMini.Success;
                modalConfig.OnOk = (cfg) =>
                {
                    try
                    {
                        repository.Delete(record.DepartmentID);
                        LoadData();
                        Message.success(this.FindForm(), "Xóa phòng ban thành công!");
                    }
                    catch (Exception ex)
                    {
                        Message.error(this.FindForm(), "Lỗi khi xoá phòng ban: " + ex.Message);
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
            ddownTP.Text = "Trưởng phòng";
            LoadData();
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string q = txtTim.Text?.Trim() ?? "";
                var departments = repository.GetAll().AsEnumerable();
                if (!string.IsNullOrWhiteSpace(q))
                {
                    departments = departments.Where(p =>
                        !string.IsNullOrEmpty(p.DepartmentName) &&
                        p.DepartmentName.IndexOf(q, StringComparison.OrdinalIgnoreCase) >= 0);
                }
                if (!string.IsNullOrWhiteSpace(ddownTP.Text) && ddownTP.Text != "Trưởng phòng")
                {
                    string empFilter = ddownTP.Text.Trim();
                    departments = departments.Where(p => p.ManagerName.ToString().Contains(empFilter));
                }
                var result = departments.ToList();
                tbPB.DataSource = result;
                if (result.Count == 0)
                {
                    Message.warn(this.FindForm(), "Không tìm thấy kết quả phù hợp.");
                }
                else
                {
                    Message.success(this.FindForm(), $"Tìm thấy {result.Count} phòng ban.");
                }
            }
            catch (Exception ex)
            {
                Message.error(this.FindForm(), "Lỗi khi tìm kiếm: " + ex.Message);
            }
        }

        private void tbPB_CellClick(object sender, TableClickEventArgs e)
        {
        }

        private void tbPB_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int sel = tbPB.SelectedIndex;
                if (sel >= 0)
                {
                    ctm1.Show(Cursor.Position);
                }
            }
        }

        private void taskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int selectedIndex = tbPB.SelectedIndex - 1;
            if (tbPB.DataSource is IList<Department> departments && selectedIndex >= 0 && selectedIndex < departments.Count)
            {
                var record = departments[selectedIndex];
                if (record == null) { Message.error(this.FindForm(), "Không thể lấy dữ liệu phòng ban được chọn!"); return; }
                int id = record.DepartmentID;
                string DepartmentName = record.DepartmentName;
                ManageEmployee frm = new ManageEmployee();
                frm.ManageEmployee_Load(id, DepartmentName);
                frm.ShowDialog();
            }
            else
            {
                Message.error(this.FindForm(), "Không thể lấy dữ liệu phòng ban được chọn!");
            }
        }

        private void chỉnhSửaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (!IsAdmin()) return; // Extra check

            int selectedIndex = tbPB.SelectedIndex - 1;
            if (tbPB.DataSource is IList<Department> departments && selectedIndex >= 0 && selectedIndex < departments.Count)
            {
                var record = departments[selectedIndex];
                if (record == null)
                {
                    Message.error(this.FindForm(), "Không thể lấy dữ liệu phòng ban được chọn!");
                    return;
                }
                // Mở form ở chế độ chỉnh sửa bằng cách truyền department hiện tại
                frmDepartment frm = new frmDepartment(record);
                if (frm.ShowDialog() == DialogResult.OK)
                {
                    var editedDepartment = frm.Tag as Department;
                    if (editedDepartment != null)
                    {
                        try
                        {
                            repository.Update(editedDepartment);
                            LoadData();
                            Message.success(this.FindForm(), "Sửa thông tin phòng ban thành công!");
                        }
                        catch (Exception ex)
                        {
                            Message.error(this.FindForm(), "Lỗi khi cập nhật phòng ban: " + ex.Message);
                        }
                    }
                }
            }
            else
            {
                Message.error(this.FindForm(), "Vui lòng chọn phòng ban cần chỉnh sửa!");
            }
        }
    }
}