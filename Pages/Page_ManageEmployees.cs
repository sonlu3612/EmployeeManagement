using AntdUI;
using EmployeeManagement.DAL.Helpers;
using EmployeeManagement.DAL.Repositories;
using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Windows.Forms;
using Message = AntdUI.Message;

namespace EmployeeManagement.Pages
{
    public partial class Page_ManageEmployees : UserControl
    {
        private readonly EmployeeRepository employeeRepository = new EmployeeRepository();
        private int employeeId;
        private string DepartmentName;

        private bool IsAdmin() => SessionManager.CurrentUser?.Roles?.Contains("Admin") ?? false;
        private bool IsManager() => SessionManager.CurrentUser?.Roles?.Contains("Quản lý phòng ban") ?? false;

        public Page_ManageEmployees()
        {
            InitializeComponent();
        }

        public void Page_ManageEmployee_Load(int id, string departmentName)
        {
            this.employeeId = id;
            this.DepartmentName = departmentName;

            tbNV.Columns.Clear();
            tbNV.Columns.Add(new Column("EmployeeID", "ID"));
            tbNV.Columns.Add(new Column("FullName", "Họ tên"));
            tbNV.Columns.Add(new Column("Position", "Chức vụ"));
            tbNV.Columns.Add(new Column("Gender", "Giới tính"));
            tbNV.Columns.Add(new Column("Address", "Địa chỉ"));
            tbNV.Columns.Add(new Column("Email", "Email"));
            tbNV.Columns.Add(new Column("Phone", "SĐT"));
            tbNV.Columns.Add(new Column("HireDateDisplay", "Ngày vào làm"));

            LoadData();
        }

        private object CreateDisplayItem(Employee emp)
        {
            return new
            {
                emp.EmployeeID,
                emp.FullName,
                emp.Position,
                emp.Gender,
                emp.Address,
                emp.Email,
                emp.Phone,
                HireDateDisplay = emp.HireDate.HasValue
                    ? emp.HireDate.Value.ToString("dd/MM/yyyy")
                    : "N/A"
            };
        }

        private void LoadData()
        {
            try
            {
                var employees = (employeeId > 0)
                    ? employeeRepository.GetForGrid2(employeeId)
                    : employeeRepository.GetAll();

                var displayList = employees.Select(e => CreateDisplayItem(e)).ToList();
                tbNV.DataSource = displayList;

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
                MessageBox.Show("Lỗi tải nhân viên: " + ex.Message);
            }
        }

        // eAdd
        private void btnAdd_Click(object sender, EventArgs eAdd)
        {
            var frm = new EmployeeManagement.Dialogs.frmEmployee();
            frm.frmEmployee_Load(this.DepartmentName);
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

        // eDel
        private void btnDelete_Click(object sender, EventArgs eDel)
        {
            int index = tbNV.SelectedIndex;
            if (index < 0)
            {
                Message.warn(this.FindForm(), "Vui lòng chọn nhân viên cần xóa!");
                return;
            }

            if (tbNV.DataSource is System.Collections.IList list && index < list.Count)
            {
                dynamic row = list[index];
                int empId = row.EmployeeID;

                var employee = employeeRepository.GetById(empId);
                if (employee == null)
                {
                    Message.error(this.FindForm(), "Không thể lấy dữ liệu nhân viên!");
                    return;
                }

                var modalConfig = Modal.config(
                    this.FindForm(),
                    "Xác nhận xoá",
                    $"Bạn có chắc muốn xoá nhân viên \"{employee.FullName}\" không?",
                    TType.Warn
                );
                modalConfig.OkText = "Xoá";
                modalConfig.CancelText = "Huỷ";
                modalConfig.OkType = TTypeMini.Success;
                modalConfig.OnOk = _ =>
                {
                    try
                    {
                        employeeRepository.Delete(empId);
                        LoadData();
                        Message.success(this.FindForm(), "Xóa nhân viên thành công!");
                    }
                    catch (Exception ex)
                    {
                        Message.error(this.FindForm(), "Lỗi khi xoá: " + ex.Message);
                    }
                    return true;
                };
                Modal.open(modalConfig);
            }
        }

        // eSync
        private void btnSync_Click(object sender, EventArgs eSync)
        {
            LoadData();
        }

        // args (ObjectNEventArgs)
        private void ddownGender_SelectedValueChanged(object sender, ObjectNEventArgs args)
        {
            string gender = args.Value?.ToString() ?? "";
            if (string.IsNullOrEmpty(gender) || gender == "Tất cả" || gender == "Giới tính")
            {
                LoadData();
                return;
            }

            try
            {
                var source = (employeeId > 0)
                    ? employeeRepository.GetForGrid2(employeeId)
                    : employeeRepository.GetAll();

                var filtered = source
                    .Where(p => p.Gender?.IndexOf(gender, StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList();

                var display = filtered.Select(e => CreateDisplayItem(e)).ToList();
                tbNV.DataSource = display;

                if (filtered.Count == 0)
                    Message.warn(this.FindForm(), "Không tìm thấy nhân viên phù hợp.");
            }
            catch (Exception ex)
            {
                Message.error(this.FindForm(), "Lỗi lọc giới tính: " + ex.Message);
            }
        }

        // eSearch
        private void btnSearch_Click(object sender, EventArgs eSearch)
        {
            string q = txtTim.Text?.Trim() ?? "";
            if (string.IsNullOrEmpty(q))
            {
                LoadData();
                return;
            }

            try
            {
                var source = (employeeId > 0)
                    ? employeeRepository.GetForGrid2(employeeId)
                    : employeeRepository.GetAll();

                var result = source
                    .Where(p => p.FullName?.IndexOf(q, StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList();

                var display = result.Select(e => CreateDisplayItem(e)).ToList();
                tbNV.DataSource = display;

                if (result.Count == 0)
                    Message.warn(this.FindForm(), "Không tìm thấy kết quả.");
                else
                    Message.success(this.FindForm(), $"Tìm thấy {result.Count} nhân viên.");
            }
            catch (Exception ex)
            {
                Message.error(this.FindForm(), "Lỗi tìm kiếm: " + ex.Message);
            }
        }

        // me (MouseEventArgs)
        private void tbNV_MouseDown(object sender, MouseEventArgs me)
        {
            if (me.Button == MouseButtons.Right)
            {
                int rowIndex = tbNV.SelectedIndex - 1;
                if (rowIndex >= 0)
                {
                    tbNV.SelectedIndex = rowIndex;
                    menuStrip.Show(Cursor.Position);
                }
            }
        }

        // eUpdate
        //private void cậpNhậtToolStripMenuItem_Click(object sender, EventArgs eUpdate)
        //{
        //    int index = tbNV.SelectedIndex;
        //    if (index < 0)
        //    {
        //        Message.warn(this.FindForm(), "Vui lòng chọn nhân viên cần cập nhật!");
        //        return;
        //    }

        //    if (tbNV.DataSource is System.Collections.IList list && index < list.Count)
        //    {
        //        dynamic row = list[index];
        //        int empId = row.EmployeeID;

        //        var employee = employeeRepository.GetById(empId);
        //        if (employee == null)
        //        {
        //            Message.error(this.FindForm(), "Không thể lấy dữ liệu nhân viên!");
        //            return;
        //        }

        //        var frm = new EmployeeManagement.Dialogs.frmEmployee(employee);
        //        frm.frmEmployee_Load(this.DepartmentName);
        //        if (frm.ShowDialog() == DialogResult.OK)
        //        {
        //            var updated = frm.Tag as Employee;
        //            if (updated != null)
        //            {
        //                try
        //                {
        //                    employeeRepository.Update(updated);
        //                    LoadData();
        //                    Message.success(this.FindForm(), "Cập nhật thành công!");
        //                }
        //                catch (Exception ex)
        //                {
        //                    Message.error(this.FindForm(), "Lỗi cập nhật: " + ex.Message);
        //                }
        //            }
        //        }
        //    }
        //}
    }
}