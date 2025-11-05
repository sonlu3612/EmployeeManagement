using AntdUI;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using EmployeeManagement.DAL.Repositories;
using EmployeeManagement.Models;

namespace EmployeeManagement.Pages
{
    public partial class Page_AssignEmployee : UserControl
    {
        private int _taskID;

        public Page_AssignEmployee()
        {
            InitializeComponent();
          

        }

        private EmployeeRepository employeeRepository = new EmployeeRepository();
        private void LoadData()
        {
            var employees = employeeRepository.GetByTask(_taskID);
            tbEmployee.DataSource = employees.ToList();
        }

        public void Page_AssignEmployee_Load(int taskID)
        {
            _taskID = taskID;
            tbEmployee.Columns.Add(new Column("EmployeeID", "ID"));
            tbEmployee.Columns.Add(new Column("FullName", "Họ và tên"));
            tbEmployee.Columns.Add(new Column("Position", "Vị trí"));
            tbEmployee.Columns.Add(new Column("Gender", "Giới tính"));
            tbEmployee.Columns.Add(new Column("DepartmentName", "Phòng ban"));

            LoadData();
        }
    }
}
