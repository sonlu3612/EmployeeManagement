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
using Message = AntdUI.Message;

namespace EmployeeManagement.Pages
{
    public partial class Page_AssignEmployee : UserControl
    {
        private int _taskID;
        private TaskRepository _taskRepository = new TaskRepository();
        public Page_AssignEmployee()
        {
            InitializeComponent();
          

        }

        private EmployeeRepository employeeRepository = new EmployeeRepository();
        private void LoadData()
        {
            try
            {
                var employees = employeeRepository.GetAll();

                //Lấy tất cả nhân viên đã gán
                var assignedEmployees = employeeRepository.GetByTask(_taskID);
                var assignedIDs = assignedEmployees.Select(e => e.EmployeeID).ToHashSet();


                var dataSource = employees.Select(e => new AssignItem
                {
                    Key = e.EmployeeID,
                    EmployeeID = e.EmployeeID,
                    FullName = e.FullName,
                    Position = e.Position ?? "Chưa có",
                    Gender = e.Gender == "1" ? "Nam" : e.Gender == "0" ? "Nữ" : "Khác",
                    DepartmentName = e.DepartmentName ?? "Chưa có",
                    IsSelected = assignedIDs.Contains(e.EmployeeID) // Đánh dấu checkbox
                }).ToList();

                tbEmployee.DataSource = dataSource;
            }
            catch(Exception ex)
            {
                Message.error(this.FindForm(),"Lỗi load: "+ex.Message);
            }
            
        }

        

        public void Page_AssignEmployee_Load(int taskID)
        {
            _taskID = taskID;
            tbEmployee.Columns.Add(new Column("EmployeeID", "ID"));
            tbEmployee.Columns.Add(new Column("FullName", "Họ và tên"));
            tbEmployee.Columns.Add(new Column("Position", "Vị trí"));
            tbEmployee.Columns.Add(new Column("Gender", "Giới tính"));
            tbEmployee.Columns.Add(new Column("DepartmentName", "Phòng ban"));
            tbEmployee.Columns.Add(new AntdUI.ColumnCheck("IsSelected")
            {
                Title = "Chọn"
            });

            LoadData();
        }

        // CLASS TẠM ĐỂ LƯU DỮ LIỆU + IsSelected
        private class AssignItem
        {
            public int Key { get; set; }
            public int EmployeeID { get; set; }
            public string FullName { get; set; }
            public string Position { get; set; }
            public string Gender { get; set; }
            public string DepartmentName { get; set; }
            public bool IsSelected { get; set; }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            try
            {
                var data = tbEmployee.DataSource as List<AssignItem>;
                var selectedIDs = data
                    .Where(x => x.IsSelected == true)
                    .Select(x => x.EmployeeID)
                    .ToList();

                if (!selectedIDs.Any())
                {
                    Message.warn(this.FindForm(), "Chưa chọn nhân viên!");
                    return;
                }
                var task = _taskRepository.GetById(_taskID);
                int assignedBy = task.CreatedBy;
                bool success = _taskRepository.AssignEmployees(_taskID, selectedIDs,assignedBy);

                if (success) 
                {
                    Message.success(this.FindForm(), $"Đã thêm {selectedIDs.Count} nhân viên vào nhiệm vụ!");
                    this.FindForm().DialogResult = DialogResult.OK;
                    this.FindForm().Close();
                }
                else
                {
                    Message.error(this.FindForm(), "Lỗi khi thêm nhân viên!");
                }

            }
            catch (Exception ex) 
            {
                Message.error(this.FindForm(),"Lỗi: "+ex.Message);

            }
        }
    }
}
