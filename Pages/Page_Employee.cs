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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Label = AntdUI.Label;
using Message = AntdUI.Message;

namespace EmployeeManagement.Pages
{
    public partial class Page_Employee : UserControl
    {
        private EmployeeRepository employeeRepository = new EmployeeRepository();
        public Page_Employee()
        {
            InitializeComponent();
        }
        private void table1_CellClick(object sender, AntdUI.TableClickEventArgs e)
        {
        }
        private void panel1_Click(object sender, EventArgs e)
        {
        }
        private bool IsInDesignMode()
        {
            return LicenseManager.UsageMode == LicenseUsageMode.Designtime
                   || (this.Site != null && this.Site.DesignMode);
        }
        private void Page_Employee_Load(object sender, EventArgs e)
        {
            if (IsInDesignMode()) return;

            // Thêm cột Avatar sử dụng ColumnSelect để hiển thị ảnh
            var avatarCol = new AntdUI.ColumnSelect("EmployeeID", "Avatar");
            avatarCol.CellType = AntdUI.SelectCellType.Icon; // Chỉ hiển thị icon (ảnh)
            avatarCol.SetAlign(AntdUI.ColumnAlign.Center); // Căn giữa
            avatarCol.SetWidth("80"); // Đặt chiều rộng cột (có thể điều chỉnh)
            tbNV.Columns.Add(avatarCol);

            tbNV.Columns.Add(new AntdUI.Column("EmployeeID", "ID"));
            tbNV.Columns.Add(new AntdUI.Column("FullName", "Họ và tên"));
            tbNV.Columns.Add(new AntdUI.Column("Gender", "Giới tính"));
            tbNV.Columns.Add(new AntdUI.Column("Email", "Email"));
            tbNV.Columns.Add(new AntdUI.Column("Role", "Vai trò"));
            tbNV.Columns.Add(new AntdUI.Column("ProjectSummary", "Dự án"));
            tbNV.Columns.Add(new AntdUI.Column("TaskSummary", "Nhiệm vụ"));

            // Lấy tất cả nhân viên để tạo Items cho ColumnSelect (mỗi Item có Tag = EmployeeID, Icon = Image từ đường dẫn tuyệt đối để test)
            var allEmployees = employeeRepository.GetAll();

            // Sử dụng loop để tạo Items với try-catch để xử lý lỗi Image.FromFile (thay vì FromStream, dùng đường dẫn tuyệt đối cho test)
            var items = new List<AntdUI.SelectItem>();
            foreach (var emp in allEmployees)
            {
                Image icon = null;
                try
                {
                    // Để test: Sử dụng đường dẫn tuyệt đối trên máy (thay bằng path thực tế của bạn, ví dụ C:\\Users\\YourName\\Pictures\\test_avatar.jpg)
                    // Nếu emp.AvatarPath là đường dẫn, dùng emp.AvatarPath; ở đây hardcode để test
                    string testPath = @"C:\Users\Admin\Hình ảnh\Saved Pictures\avt1.jpg"; // Thay bằng đường dẫn tuyệt đối thực tế trên máy bạn
                    if (File.Exists(testPath))
                    {
                        icon = Image.FromFile(testPath);
                    }
                    else
                    {
                        Console.WriteLine($"File không tồn tại: {testPath}");
                    }
                }
                catch (Exception ex)
                {
                    // Xử lý lỗi: Có thể log hoặc bỏ qua, set icon = null hoặc ảnh default
                    Console.WriteLine($"Lỗi load avatar từ đường dẫn tuyệt đối cho EmployeeID {emp.EmployeeID}: {ex.Message}");
                    // Optional: icon = Properties.Resources.DefaultAvatar; // Nếu có ảnh default
                }
                items.Add(new AntdUI.SelectItem(0, icon, emp.FullName ?? "", emp.EmployeeID));

            }
            avatarCol.Items = items;

            LoadData();
        }
        private void LoadData()
        {
            tbNV.DataSource = employeeRepository.GetForGrid();
            if (ddownGender.Items.Count == 0)
            {
                ddownGender.Items.Add("Tất cả");
                ddownGender.Items.Add("Nam");
                ddownGender.Items.Add("Nữ");
            }
            ddownGender.Text = "Giới tính";
            if (ddownRole.Items.Count == 0)
            {
                ddownRole.Items.Add("Tất cả");
                ddownRole.Items.Add("Admin");
                ddownRole.Items.Add("Employee");
                ddownRole.Items.Add("Manager");
            }
            ddownRole.Text = "Vai trò";
        }
        private void btnAdd_Click(object sender, EventArgs e)
        {
            EmployeeManagement.Dialogs.frmEmployee frm = new EmployeeManagement.Dialogs.frmEmployee();
            if (frm.ShowDialog() == DialogResult.OK)
            {
                var newEmployee = frm.Tag as Employee;
                if (newEmployee != null)
                {
                    employeeRepository.Insert(newEmployee);
                    LoadData();
                    Message.success(this.FindForm(), "Thêm nhân viên thành công!");
                }
            }
        }
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
                        LoadData();
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
            LoadData();
        }
        private void ddownGender_SelectedValueChanged(object sender, ObjectNEventArgs e)
        {
            ddownGender.Text = e.Value?.ToString() ?? "";
            if (ddownGender.Text == "Tất cả" || string.IsNullOrEmpty(ddownGender.Text))
            {
                LoadData();
                return;
            }
            var filteredProjects = employeeRepository.GetForGrid()
                .Where(p => p.Gender.ToString().Contains(ddownGender.Text))
                .ToList();
            tbNV.DataSource = filteredProjects;
        }
        private void ddownRole_SelectedValueChanged(object sender, ObjectNEventArgs e)
        {
            ddownRole.Text = e.Value?.ToString() ?? "";
            if (ddownRole.Text == "Tất cả" || string.IsNullOrEmpty(ddownRole.Text))
            {
                LoadData();
                return;
            }
            var filteredProjects = employeeRepository.GetForGrid()
                .Where(p => p.Role.ToString().Contains(ddownRole.Text))
                .ToList();
            tbNV.DataSource = filteredProjects;
        }
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string q = txtTim.Text?.Trim() ?? "";
                var employees = employeeRepository.GetAll().AsEnumerable();
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
                if (!string.IsNullOrWhiteSpace(ddownRole.Text) && ddownRole.Text != "Vai trò")
                {
                    string statusFilter = ddownRole.Text.Trim();
                    employees = employees.Where(p => !string.IsNullOrEmpty(p.Role) &&
                                                   p.Role.IndexOf(statusFilter, StringComparison.OrdinalIgnoreCase) >= 0);
                }
                var result = employees.ToList();
                tbNV.DataSource = result;
                if (result.Count == 0)
                {
                    Message.warn(this.FindForm(), "Không tìm thấy kết quả phù hợp.");
                }
                else
                {
                    Message.success(this.FindForm(), $"Tìm thấy {result.Count} nhân viên.");
                }
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
                if (record == null) { Message.error(this.FindForm(), "Không thể lấy dữ liệu dự án được chọn!"); return; }
                int id = record.EmployeeID;
                frmAssignTask frm = new frmAssignTask();
                frm.frmAssignTask_Load(id);
                frm.ShowDialog();
            }
            else
            {
                Message.error(this.FindForm(), "Không thể lấy dữ liệu dự án được chọn!");
            }
        }
    }
}