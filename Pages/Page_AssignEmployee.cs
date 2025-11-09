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
                var assignedEmployees = employeeRepository.GetByTask(_taskID);
                var assignedIDs = assignedEmployees.Select(e => e.EmployeeID).ToHashSet();

                var dataSource = employees.Select(e => new AssignItem
                {
                    Key = e.EmployeeID,
                    EmployeeID = e.EmployeeID,
                    FullName = e.FullName,
                    Position = e.Position ?? "Chưa có",
                    Gender = e.Gender,
                    DepartmentName = e.DepartmentName ?? "Chưa có",
                    IsSelected = assignedIDs.Contains(e.EmployeeID)
                }).ToList();

                tbEmployee.DataSource = dataSource;

                ddownPB.Items.Clear();
                ddownPB.Items.Add("Tất cả");

                var departmentNames = employees
                    .Select(e => e.DepartmentName)
                    .Where(name => !string.IsNullOrWhiteSpace(name))
                    .Distinct()
                    .OrderBy(name => name)
                    .ToList();

                foreach (var dep in departmentNames)
                {
                    ddownPB.Items.Add(dep);
                }
            }
            catch (Exception ex)
            {
                Message.error(this.FindForm(), "Lỗi load dữ liệu: " + ex.Message);
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
                var ds = tbEmployee.DataSource;
                if (ds == null)
                {
                    Message.warn(this.FindForm(), "Không có dữ liệu nhân viên để xử lý.");
                    return;
                }

                // Hỗ trợ nhiều kiểu DataSource: IEnumerable<T>, DataTable, IList, v.v.
                var selectedIDs = new List<int>();

                // 1) Nếu là DataTable
                if (ds is DataTable dt)
                {
                    foreach (DataRow r in dt.Rows)
                    {
                        // nếu có cột IsSelected và EmployeeID
                        if (dt.Columns.Contains("IsSelected") && dt.Columns.Contains("EmployeeID"))
                        {
                            bool isSel = r["IsSelected"] != DBNull.Value && Convert.ToBoolean(r["IsSelected"]);
                            if (isSel)
                            {
                                selectedIDs.Add(Convert.ToInt32(r["EmployeeID"]));
                            }
                        }
                    }
                }
                else if (ds is System.Collections.IEnumerable enumerable)
                {
                    foreach (var item in enumerable)
                    {
                        if (item == null) continue;

                        var t = item.GetType();

                        // tìm property IsSelected (bool)
                        var pIsSelected = t.GetProperty("IsSelected");
                        // tìm property EmployeeID hoặc Key
                        var pEmpId = t.GetProperty("EmployeeID") ?? t.GetProperty("Key") ?? t.GetProperty("Id") ?? t.GetProperty("ID");

                        bool isSel = false;
                        int empId = 0;

                        if (pIsSelected != null)
                        {
                            var val = pIsSelected.GetValue(item);
                            if (val != null)
                            {
                                // an toàn convert
                                bool.TryParse(val.ToString(), out isSel);
                            }
                        }

                        if (pEmpId != null)
                        {
                            var val = pEmpId.GetValue(item);
                            if (val != null && int.TryParse(val.ToString(), out int tmpId))
                            {
                                empId = tmpId;
                            }
                        }

                        if (isSel && empId > 0)
                        {
                            selectedIDs.Add(empId);
                        }
                    }
                }

                if (!selectedIDs.Any())
                {
                    Message.warn(this.FindForm(), "Chưa chọn nhân viên!");
                    return;
                }

                var task = _taskRepository.GetById(_taskID);
                if (task == null)
                {
                    Message.error(this.FindForm(), "Không tìm thấy nhiệm vụ.");
                    return;
                }

                int assignedBy = task.CreatedBy; // đã kiểm tra task != null
                bool success = _taskRepository.AssignEmployees(_taskID, selectedIDs, assignedBy);

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
                Message.error(this.FindForm(), "Lỗi: " + ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string keyword = txtTim.Text?.Trim() ?? "";
                var ds = tbEmployee.DataSource as List<AssignItem>;

                if (ds == null || ds.Count == 0)
                {
                    Message.warn(this.FindForm(), "Không có dữ liệu để tìm kiếm.");
                    return;
                }

                if (string.IsNullOrWhiteSpace(keyword))
                {
                    LoadData();
                    return;
                }

                var filtered = ds
                    .Where(e1 => e1.FullName != null && e1.FullName.IndexOf(keyword, StringComparison.OrdinalIgnoreCase) >= 0)
                    .ToList();

                tbEmployee.DataSource = filtered;

                if (filtered.Count == 0)
                    Message.warn(this.FindForm(), "Không tìm thấy nhân viên phù hợp.");
                else
                    Message.success(this.FindForm(), $"Tìm thấy {filtered.Count} nhân viên phù hợp!");
            }
            catch (Exception ex)
            {
                Message.error(this.FindForm(), "Lỗi tìm kiếm: " + ex.Message);
            }
        }


        private void btnSync_Click(object sender, EventArgs e)
        {
            txtTim.Text = string.Empty;
            ddownPB.Text = "Phòng ban";
            try
            {
                LoadData();
                Message.success(this.FindForm(), "Đã đồng bộ lại danh sách nhân viên!");
            }
            catch (Exception ex)
            {
                Message.error(this.FindForm(), "Lỗi đồng bộ: " + ex.Message);
            }
        }


        private void ddownPB_SelectedValueChanged(object sender, ObjectNEventArgs e)
        {
            try
            {
                string selected = e.Value?.ToString() ?? "";

                if (string.IsNullOrWhiteSpace(selected) || selected.Equals("Tất cả", StringComparison.OrdinalIgnoreCase))
                {
                    LoadData();
                    return;
                }

                var allEmployees = employeeRepository.GetAll();
                var assignedEmployees = employeeRepository.GetByTask(_taskID);
                var assignedIDs = assignedEmployees.Select(emp => emp.EmployeeID).ToHashSet();

                var filtered = allEmployees
                    .Where(emp => emp.DepartmentName != null && emp.DepartmentName.Equals(selected, StringComparison.OrdinalIgnoreCase))
                    .Select(e1 => new AssignItem
                    {
                        Key = e1.EmployeeID,
                        EmployeeID = e1.EmployeeID,
                        FullName = e1.FullName,
                        Position = e1.Position ?? "Chưa có",
                        Gender = e1.Gender,
                        DepartmentName = e1.DepartmentName ?? "Chưa có",
                        IsSelected = assignedIDs.Contains(e1.EmployeeID)
                    })
                    .ToList();

                tbEmployee.DataSource = filtered;

                Message.success(this.FindForm(), $"Hiển thị {filtered.Count} nhân viên trong phòng '{selected}'.");
            }
            catch (Exception ex)
            {
                Message.error(this.FindForm(), "Lỗi khi lọc phòng ban: " + ex.Message);
            }
        }

        private Table.CellStyleInfo tbEmployee_SetRowStyle(object sender, TableSetRowStyleEventArgs e)
        {
            if (e.Index % 2 == 0)
            {
                return new AntdUI.Table.CellStyleInfo
                {
                    BackColor = Color.FromArgb(208, 231, 252)
                };
            }
            return null;
        }
    }
}