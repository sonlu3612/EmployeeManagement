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
        private EmployeeRepository employeeRepository = new EmployeeRepository();
        private List<AssignItem> _fullData;
        private string _currentDepartment = "Tất cả";
        private string _currentSelect = "Tất cả";
        private string _currentKeyword = "";
        public Page_AssignEmployee()
        {
            InitializeComponent();
        }
        private void LoadData()
        {
            try
            {
                var employees = employeeRepository.GetAll();
                var assignedEmployees = employeeRepository.GetByTask(_taskID);
                var assignedIDs = assignedEmployees.Select(e => e.EmployeeID).ToHashSet();
                var assignedStatusDict = assignedEmployees.ToDictionary(e => e.EmployeeID, e => e.CompletionStatus ?? "Pending");
                _fullData = employees.Select(e => new AssignItem
                {
                    Key = e.EmployeeID,
                    EmployeeID = e.EmployeeID,
                    FullName = e.FullName,
                    Position = e.Position ?? "Chưa có",
                    Gender = e.Gender,
                    DepartmentName = e.DepartmentName ?? "Chưa có",
                    CompletionStatus = assignedStatusDict.TryGetValue(e.EmployeeID, out string dbStatus)
                        ? (dbStatus == "Pending" ? "Chưa làm" : dbStatus == "Completed" ? "Đã làm" : dbStatus)
                        : "Không",
                    IsSelected = assignedIDs.Contains(e.EmployeeID)
                }).ToList();
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
                ddownSelect.Items.Clear();
                ddownSelect.Items.Add("Tất cả");
                ddownSelect.Items.Add("Đã chọn");
                ddownSelect.Items.Add("Chưa chọn");
                _currentDepartment = "Tất cả";
                _currentSelect = "Tất cả";
                _currentKeyword = "";
                txtTim.Text = "";
                ApplyFilters();
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
            tbEmployee.Columns.Add(new Column("CompletionStatus", "Trạng thái"));
            tbEmployee.Columns.Add(new AntdUI.ColumnCheck("IsSelected", "Phân công")
            {
                Title = "Chọn"
            });
            LoadData();
        }
        private class AssignItem
        {
            public int Key { get; set; }
            public int EmployeeID { get; set; }
            public string FullName { get; set; }
            public string Position { get; set; }
            public string Gender { get; set; }
            public string DepartmentName { get; set; }
            public string CompletionStatus { get; set; }
            public bool IsSelected { get; set; }
        }
        private void ApplyFilters()
        {
            if (_fullData == null) return;
            var filtered = _fullData.ToList();
            // Apply department filter
            if (!_currentDepartment.Equals("Tất cả", StringComparison.OrdinalIgnoreCase))
            {
                filtered = filtered.Where(item => item.DepartmentName?.Equals(_currentDepartment, StringComparison.OrdinalIgnoreCase) ?? false).ToList();
            }
            // Apply select filter
            if (_currentSelect.Equals("Đã chọn", StringComparison.OrdinalIgnoreCase))
            {
                filtered = filtered.Where(item => item.IsSelected).ToList();
            }
            else if (_currentSelect.Equals("Chưa chọn", StringComparison.OrdinalIgnoreCase))
            {
                filtered = filtered.Where(item => !item.IsSelected).ToList();
            }
            // Apply keyword filter
            if (!string.IsNullOrWhiteSpace(_currentKeyword))
            {
                filtered = filtered.Where(item => item.FullName != null && item.FullName.IndexOf(_currentKeyword, StringComparison.OrdinalIgnoreCase) >= 0).ToList();
            }
            tbEmployee.DataSource = filtered;
            if (filtered.Count == 0)
            {
                Message.warn(this.FindForm(), "Không tìm thấy nhân viên phù hợp.");
            }
            else
            {
                Message.success(this.FindForm(), $"Tìm thấy {filtered.Count} nhân viên phù hợp!");
            }
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
                int assignedBy = task.CreatedBy;
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
                _currentKeyword = txtTim.Text?.Trim() ?? "";
                ApplyFilters();
            }
            catch (Exception ex)
            {
                Message.error(this.FindForm(), "Lỗi tìm kiếm: " + ex.Message);
            }
        }
        private void btnSync_Click(object sender, EventArgs e)
        {
            try
            {
                ddownPB.Text = "Phòng ban";
                ddownSelect.Text = "Trạng thái";
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
                ddownPB.Text = e.Value.ToString();
                _currentDepartment = e.Value?.ToString() ?? "Tất cả";
                ApplyFilters();
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
        private void ddownSelect_SelectedValueChanged(object sender, ObjectNEventArgs e)
        {
            try
            {
                ddownSelect.Text = e.Value?.ToString();
                _currentSelect = e.Value?.ToString() ?? "Tất cả";
                ApplyFilters();
            }
            catch (Exception ex)
            {
                Message.error(this.FindForm(), "Lỗi khi lọc theo lựa chọn: " + ex.Message);
            }
        }
    }
}