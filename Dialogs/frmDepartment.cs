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

        // Constructor cho thêm mới
        public frmDepartment()
        {
            InitializeComponent();
        }

        // Constructor cho chỉnh sửa: truyền department cần sửa
        public frmDepartment(Department departmentToEdit) : this()
        {
            if (departmentToEdit == null) throw new ArgumentNullException(nameof(departmentToEdit));
            _isEdit = true;
            _editingDepartment = departmentToEdit;
        }

        private void frmDepartment_Load(object sender, EventArgs e)
        {
            // Load danh sách nhân viên vào combobox (FullName)
            var employees = employeeRepository.GetAll().ToList();
            cbTP.Items.Clear();
            foreach (var em in employees)
            {
                cbTP.Items.Add(em.FullName);
            }

            // Nếu là chế độ chỉnh sửa thì prefill giá trị
            if (_isEdit && _editingDepartment != null)
            {
                txtTenPB.Text = _editingDepartment.DepartmentName;
                txtDescription.Text = _editingDepartment.Description;

                // ưu tiên chọn theo ManagerID nếu có
                if (_editingDepartment.ManagerID > 0)
                {
                    var emp = employees.FirstOrDefault(x => x.EmployeeID == _editingDepartment.ManagerID);
                    if (emp != null)
                    {
                        cbTP.Text = emp.FullName;
                    }
                    else
                    {
                        // fallback: dùng ManagerName
                        cbTP.Text = _editingDepartment.ManagerName;
                    }
                }
                else if (!string.IsNullOrWhiteSpace(_editingDepartment.ManagerName))
                {
                    cbTP.Text = _editingDepartment.ManagerName;
                }

                // thay đổi text nút nếu muốn
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
            // Validate tên phòng ban
            if (string.IsNullOrWhiteSpace(txtTenPB?.Text))
            {
                Message.warn(this.FindForm(), "Vui lòng nhập tên phòng ban!");
                return;
            }

            // Validate trưởng phòng
            if (string.IsNullOrWhiteSpace(cbTP?.Text))
            {
                Message.warn(this.FindForm(), "Vui lòng chọn trưởng phòng!");
                return;
            }

            // Tìm employee theo FullName (lưu ý trùng tên)
            var employee = employeeRepository.GetAll()
                .FirstOrDefault(d => d.FullName == cbTP.Text);

            if (_isEdit && _editingDepartment != null)
            {
                // Cập nhật đối tượng hiện có
                _editingDepartment.DepartmentName = txtTenPB.Text.Trim();
                _editingDepartment.Description = txtDescription.Text?.Trim();
                _editingDepartment.ManagerName = cbTP.Text;
                _editingDepartment.ManagerID = employee != null ? employee.EmployeeID : 0; // nếu model dùng int? => có thể gán null

                // Trả đối tượng đã sửa về caller
                this.Tag = _editingDepartment;
            }
            else
            {
                // Thêm mới
                var newDepartment = new Department()
                {
                    DepartmentName = txtTenPB.Text.Trim(),
                    Description = txtDescription.Text?.Trim(),
                    ManagerName = cbTP.Text,
                    ManagerID = employee != null ? employee.EmployeeID : 0
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
