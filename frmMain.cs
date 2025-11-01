using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using AntdUI;
using EmployeeManagement.DAL.Interfaces;
using EmployeeManagement.DAL.Repositories;
using EmployeeManagement.Models;

namespace EmployeeManagement
{
    public partial class frmMain : AntdUI.Window
    {
        private readonly IRepository<Employee> _employeeRepo = new EmployeeRepository();
        private Employee _selectedEmployee;

        public frmMain()
        {
            InitializeComponent();
            InitNhanVienTable();

            // Gán sự kiện
            //btnDelete.Click += BtnDelete_Click;
            //tblNhanVien.Click += TblNhanVien_Click;
        }

        private void InitNhanVienTable()
        {
            // Xóa cột cũ
            tblNhanVien.Columns.Clear();

            // Thêm cột mới
            tblNhanVien.Columns.Add(new Column("EmployeeID", "Mã NV"));
            tblNhanVien.Columns.Add(new Column("FullName", "Họ tên"));
            tblNhanVien.Columns.Add(new Column("Email", "Email"));
            tblNhanVien.Columns.Add(new Column("Phone", "SĐT"));
            tblNhanVien.Columns.Add(new Column("DepartmentID", "Phòng"));
            tblNhanVien.Columns.Add(new Column("Address", "Địa chỉ"));
            tblNhanVien.Columns.Add(new Column("HireDate", "Ngày vào làm"));

            // Nạp dữ liệu
            LoadNhanVienData();
        }

        private void LoadNhanVienData()
        {
            try
            {
                // Lấy danh sách nhân viên active
                var employees = _employeeRepo.GetAll()
                                             .Where(emp => emp.IsActive) // chỉ nhân viên chưa xóa
                                             .ToList();

                tblNhanVien.DataSource = employees;
                _selectedEmployee = null; // reset chọn nhân viên
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi tải dữ liệu nhân viên: {ex.Message}");
            }
        }

        // Khi click vào dòng trong bảng
        private void TblNhanVien_Click(object sender, EventArgs e)
        {
            // Lấy danh sách nguồn dữ liệu
            var employees = tblNhanVien.DataSource as List<Employee>;
            if (employees == null || employees.Count == 0) return;

            // Lấy index dòng được chọn
            int index = tblNhanVien.SelectedIndex; // không trừ 1 nữa
            if (index < 0 || index >= employees.Count) return;

            // Lấy nhân viên tương ứng
            _selectedEmployee = employees[index];

            // Debug
            Console.WriteLine($"Đã chọn nhân viên: {_selectedEmployee.EmployeeID} - {_selectedEmployee.FullName}");
        }

        // Nút Delete
        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (_selectedEmployee == null)
            {
                MessageBox.Show("Vui lòng chọn nhân viên cần xóa!");
                return;
            }

            var confirm = MessageBox.Show(
                $"Bạn có chắc muốn xóa nhân viên '{_selectedEmployee.FullName}' không?",
                "Xác nhận xóa",
                MessageBoxButtons.OKCancel,
                MessageBoxIcon.Question
            );

            if (confirm == DialogResult.OK)
            {
                try
                {
                    Console.WriteLine($"Xóa nhân viên ID: {_selectedEmployee.EmployeeID}");

                    // Thực hiện xóa mềm (IsActive = 0)
                    _employeeRepo.Delete(_selectedEmployee.EmployeeID);

                    // Reload dữ liệu, nhân viên vừa xóa sẽ biến mất
                    LoadNhanVienData();

                    MessageBox.Show("Đã xóa nhân viên thành công!");
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Lỗi khi xóa: {ex.Message}");
                }
            }
        }

        private void tblNhanVien_CellClick(object sender, TableClickEventArgs e)
        {
            if (e.RowIndex < 0) return;  // Bỏ qua nếu click header hoặc ngoài dữ liệu

            var employees = tblNhanVien.DataSource as List<Employee>;
            if (employees == null || employees.Count == 0 || e.RowIndex >= employees.Count) return;

            _selectedEmployee = employees[e.RowIndex];
        }
    }
}
