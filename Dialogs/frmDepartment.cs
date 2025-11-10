using AntdUI;
using EmployeeManagement.DAL.Interfaces;
using EmployeeManagement.DAL.Repositories;
using EmployeeManagement.Models;
using System;
using System.Linq;
using System.Windows.Forms;
using Message = AntdUI.Message;

namespace EmployeeManagement.Dialogs
{
    public partial class frmDepartment : Form
    {
        private readonly IRepository<Department> repository = new DepartmentRepository();
        private readonly IRepository<Employee> employeeRepository = new EmployeeRepository();

        private bool _isEdit = false;
        private Department _editingDepartment = null;

        public frmDepartment()
        {
            InitializeComponent();
        }

        public frmDepartment(Department departmentToEdit) : this()
        {
            _isEdit = true;
            _editingDepartment = departmentToEdit ?? throw new ArgumentNullException(nameof(departmentToEdit));
        }

        private void frmDepartment_Load(object sender, EventArgs e)
        {
            var employees = employeeRepository.GetAll().ToList();
            cbTP.Items.Clear();
            foreach (var em in employees)
            {
                cbTP.Items.Add(em.FullName);
            }

            if (_isEdit && _editingDepartment != null)
            {
                txtTenPB.Text = _editingDepartment.DepartmentName;
                txtDescription.Text = _editingDepartment.Description;

                if (_editingDepartment.ManagerID > 0)
                {
                    var emp = employees.FirstOrDefault(x => x.EmployeeID == _editingDepartment.ManagerID);
                    if (emp != null)
                    {
                        cbTP.Text = emp.FullName;
                    }
                    else
                    {
                        cbTP.Text = _editingDepartment.ManagerName;
                    }
                }
                else if (!string.IsNullOrWhiteSpace(_editingDepartment.ManagerName))
                {
                    cbTP.Text = _editingDepartment.ManagerName;
                }

                try
                {
                    button1.Text = "Cập nhật";
                }
                catch { }
            }
            else
            {
                try
                {
                    button1.Text = "Thêm";
                }
                catch { }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            var name = txtTenPB?.Text?.Trim() ?? string.Empty;
            var desc = txtDescription?.Text?.Trim() ?? string.Empty;
            var managerName = cbTP?.Text?.Trim() ?? string.Empty;

            if (string.IsNullOrWhiteSpace(name))
            {
                Message.warn(this.FindForm(), "Vui lòng nhập tên phòng ban!");
                return;
            }

            if (name.Length > 100)
            {
                Message.warn(this.FindForm(), "Tên phòng ban không được vượt quá 100 ký tự.");
                return;
            }

            if (desc.Length > 500)
            {
                Message.warn(this.FindForm(), "Mô tả không được vượt quá 500 ký tự.");
                return;
            }

            if (string.IsNullOrWhiteSpace(managerName))
            {
                Message.warn(this.FindForm(), "Vui lòng chọn trưởng phòng!");
                return;
            }

            var employees = employeeRepository.GetAll().ToList();
            var employee = employees.FirstOrDefault(d => d.FullName == managerName);
            if (employee == null)
            {
                Message.warn(this.FindForm(), "Trưởng phòng không hợp lệ. Vui lòng chọn một nhân viên hợp lệ từ danh sách.");
                return;
            }

            var existing = repository.GetAll()
                .FirstOrDefault(d => d.DepartmentName.Equals(name, StringComparison.OrdinalIgnoreCase)
                    && (!_isEdit || d.DepartmentID != (_editingDepartment?.DepartmentID ?? 0)));

            if (existing != null)
            {
                Message.warn(this.FindForm(), "Tên phòng ban đã tồn tại. Vui lòng chọn tên khác.");
                return;
            }

            if (_isEdit && _editingDepartment != null)
            {
                _editingDepartment.DepartmentName = name;
                _editingDepartment.Description = desc;
                _editingDepartment.ManagerName = employee.FullName;
                _editingDepartment.ManagerID = employee.EmployeeID;

                this.Tag = _editingDepartment;
            }
            else
            {
                var newDepartment = new Department()
                {
                    DepartmentName = name,
                    Description = desc,
                    ManagerName = employee.FullName,
                    ManagerID = employee.EmployeeID
                };

                this.Tag = newDepartment;
            }

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnHuy_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void cbTP_SelectedValueChanged(object sender, AntdUI.ObjectNEventArgs e)
        {
            cbTP.Text = e.Value?.ToString();
        }
    }
}
