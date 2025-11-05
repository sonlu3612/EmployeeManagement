using AntdUI;
using EmployeeManagement.DAL.Repositories;
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
    public partial class Page_ManageEmployees : UserControl
    {
        private EmployeeRepository employeeRepository = new EmployeeRepository();
        private int employeeId;
        public Page_ManageEmployees()
        {
            InitializeComponent();
        }

        private void table1_CellClick(object sender, AntdUI.TableClickEventArgs e)
        {

        }

        private void panel1_Click(object sender, EventArgs e)
        {

        }

        public void Page_ManageEmployee_Load(int id)
        {
            this.employeeId = id;

            tbNV.Columns.Add(new Column("EmployeeID", "ID"));
            tbNV.Columns.Add(new Column("FullName", "Hợ tên"));
            tbNV.Columns.Add(new Column("Position", "Chức vụ"));
            tbNV.Columns.Add(new Column("Gender", "Giới tính"));
            tbNV.Columns.Add(new Column("Address", "Địa chỉ"));
            tbNV.Columns.Add(new Column("Email", "Email"));
            tbNV.Columns.Add(new Column("Phone", "SĐT"));
            tbNV.Columns.Add(new Column("HireDate", "Ngày vào làm"));

            LoadData();
        }

        private void LoadData()
        {
            tbNV.DataSource = employeeRepository.GetForGrid2(employeeId); ;
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

                if (!string.IsNullOrWhiteSpace(ddownRole.Text) && ddownRole.Text != "Quyền")
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
    }
}
