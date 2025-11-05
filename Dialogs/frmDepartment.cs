using EmployeeManagement.DAL.Interfaces;
using EmployeeManagement.DAL.Repositories;
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
using System.Xml.Linq;

namespace EmployeeManagement.Dialogs
{
    public partial class frmDepartment : Form
    {
        private readonly IRepository<Department> repository = new DepartmentRepository();
        private readonly IRepository<Employee> employeeRepository = new EmployeeRepository();
        public frmDepartment()
        {
            InitializeComponent();
        }

        private void frmDepartment_Load(object sender, EventArgs e)
        {
            var employees = employeeRepository.GetAll();
            foreach (var em in employees)
            {
                string display = $"{em.FullName}";
                cbTP.Items.Add(display);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (txtTenPB == null || string.IsNullOrWhiteSpace(txtTenPB.Text))
            {
                AntdUI.Message.warn(this.FindForm(), "Vui lòng nhập tên phòng ban!");
                return;
            }
            if (cbTP == null)
            {
                AntdUI.Message.warn(this.FindForm(), "Vui lòng chọn trưởng phòng!");
                return;
            }

            var employee = employeeRepository.GetAll()
                .FirstOrDefault(d => d.FullName == cbTP.Text);

            var newDepartment = new Department()
            {
                DepartmentName = txtTenPB.Text,
                Description = txtDescription.Text,
                ManagerName = cbTP.Text,
                ManagerID = employee.EmployeeID
            };
            this.Tag = newDepartment;
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
