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
        private string DepartmentName;
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

        public void Page_ManageEmployee_Load(int id, string DepartmentName)
        {
            this.employeeId = id;
            this.DepartmentName = DepartmentName;

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
            tbNV.DataSource = employeeRepository.GetForGrid2(employeeId);
            if (ddownGender.Items.Count == 0)
            {
                ddownGender.Items.Add("Tất cả");
                ddownGender.Items.Add("Nam");
                ddownGender.Items.Add("Nữ");
            }
            ddownGender.Text = "Giới tính";
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            EmployeeManagement.Dialogs.frmEmployee frm = new EmployeeManagement.Dialogs.frmEmployee();
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
            string genderFilter = ddownGender.Text.Trim();

            if (string.IsNullOrEmpty(genderFilter) || genderFilter == "Tất cả" || genderFilter == "Giới tính")
            {
                LoadData();
                return;
            }

            try
            {
                IEnumerable<Employee> source;
                try
                {
                    source = (employeeId > 0)
                        ? employeeRepository.GetForGrid2(employeeId)
                        : employeeRepository.GetAll();
                }
                catch
                {
                    source = employeeRepository.GetAll();
                }

                var filtered = source
                    .Where(p => !string.IsNullOrEmpty(p.Gender) &&
                                p.Gender.IndexOf(genderFilter, StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList();

                tbNV.DataSource = filtered;

                if (filtered.Count == 0)
                    Message.warn(this.FindForm(), "Không tìm thấy nhân viên phù hợp với giới tính đã chọn.");
            }
            catch (Exception ex)
            {
                Message.error(this.FindForm(), "Lỗi khi lọc giới tính: " + ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string q = txtTim.Text?.Trim() ?? "";
                if (string.IsNullOrEmpty(q))
                {
                    LoadData();
                    return;
                }

                IEnumerable<Employee> source;
                try
                {
                    source = (employeeId > 0) ? employeeRepository.GetForGrid2(employeeId) : employeeRepository.GetAll();
                    if (source == null)
                    {
                        source = employeeRepository.GetAll();
                    }
                }
                catch
                {
                    source = employeeRepository.GetAll();
                }

                var result = source
                    .Where(p => !string.IsNullOrEmpty(p.FullName) &&
                                p.FullName.IndexOf(q, StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList();

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

    }
}
