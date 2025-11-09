using AntdUI;
using EmployeeManagement.DAL.Repositories;
using EmployeeManagement.Dialogs;
using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Threading.Tasks;
using System.Windows.Forms;
using Message = AntdUI.Message;
using Task = EmployeeManagement.Models.Task;
namespace EmployeeManagement.Pages
{
    public partial class Page_ManageTask : UserControl
    {
        private TaskRepository _taskRepository = new TaskRepository();
        private EmployeeRepository employeeRepository = new EmployeeRepository();
        private int projectId;
        private List<Task> _cachedTasks = new List<Task>();
        //private User _user =
        public Page_ManageTask()
        {
            InitializeComponent();
        }
        public void Page_ManageTask_Load(int id)
        {
            this.projectId = id;
            tbTask.Columns.Clear();
            tbTask.Columns.Add(new Column("ProjectName", "Tên dự án"));
            tbTask.Columns.Add(new Column("TaskName", "Tên nhiệm vụ"));
            tbTask.Columns.Add(new Column("Description", "Mô tả"));
            tbTask.Columns.Add(new Column("EmployeeName", "Người tạo"));
            tbTask.Columns.Add(new Column("Deadline", "Hết hạn"));
            tbTask.Columns.Add(new Column("Status", "Trạng thái"));
            tbTask.Columns.Add(new Column("Priority", "Độ ưu tiên"));
            tbTask.Columns.Add(new Column("Progress", "Tiến độ"));
            tbTask.Columns.Add(new Column("CreatedDate", "Ngày tạo"));
            LoadData();
            loadEmployeesName();
        }
        private void LoadData()
        {
            _cachedTasks = _taskRepository.GetByProject(this.projectId).ToList();
            foreach (var task in _cachedTasks)
            {
                var creator = employeeRepository.GetById(task.CreatedBy);
                task.EmployeeName = creator?.FullName ?? "Unknown";
            }
            tbTask.DataSource = _cachedTasks;
            try
            {
                if (ddownStatus.Items.Count == 0)
                {
                    ddownStatus.Items.Clear();
                    ddownStatus.Items.Insert(0, "Tất cả");
                    var statusList = _cachedTasks
                        .Select(t => t.Status ?? "")
                        .Where(s => !string.IsNullOrWhiteSpace(s))
                        .Distinct()
                        .OrderBy(x => x)
                        .ToArray();
                    if (statusList.Length > 0)
                        ddownStatus.Items.AddRange(statusList);
                    ddownStatus.Items.Add("Quá hạn");
                }
            }
            catch
            {
                // ignore
            }
        }
        private void loadEmployeesName()
        {
            var employees = employeeRepository.GetAll();
            ddownEmployee.Items.Clear();
            ddownEmployee.Items.Insert(0, "Tất cả");
            foreach (var emp in employees)
            {
                string displayText = $"{emp.EmployeeID} - {emp.FullName}";
                ddownEmployee.Items.Add(displayText);
            }
        }

        #region Add / Edit / Delete / Click
        private void btnAdd_Click(object sender, EventArgs e)
        {
            frmTask frmTask = new frmTask(projectId);
            //frmTask.ShowDialog();
            if (frmTask.ShowDialog() == DialogResult.OK)
            {
                LoadData();
                //frmTask.Close();
            }
        }
        private void btnThem_Click(object sender, EventArgs e)
        {
            //frmTask frm = new frmTask(projectId); // nếu constructor có param projectId
            //if (frm.ShowDialog() == DialogResult.OK)
            //{
            // var newTask = frm.Tag as Task;
            // if (newTask != null)
            // {
            // _taskRepository.Insert(newTask);
            // LoadData();
            // Message.success(this.FindForm(), "Thêm task thành công!");
            // }
            //}
        }
        private void tbTask_Click(object sender, EventArgs e)
        {
            //var selectedIndex = tbTask.SelectedIndex;
            //if (tbTask.DataSource is List<Task> tasks && selectedIndex >= 0 && selectedIndex < tasks.Count)
            //{
            // var task = tasks[selectedIndex];
            // // Mở form edit (giả sử frmTask có constructor từ Task)
            // frmTask frm = new frmTask(task);
            // if (frm.ShowDialog() == DialogResult.OK)
            // {
            // var updatedTask = frm.Tag as Task;
            // if (updatedTask != null)
            // {
            // _taskRepository.Update(updatedTask);
            // LoadData();
            // Message.success(this.FindForm(), "Cập nhật task thành công!");
            // }
            // }
            //}
            //else
            //{
            // Message.warn(this.FindForm(), "Vui lòng chọn task cần sửa!");
            //}
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            var selectedIndex = tbTask.SelectedIndex - 1;
            if (selectedIndex < 0)
            {
                Message.warn(this.FindForm(), "Vui lòng chọn nhiệm vụ cần xóa!");
                return;
            }
            if (tbTask.DataSource is IList<Task> tasks && selectedIndex < tasks.Count)
            {
                var record = tasks[selectedIndex];
                if (record == null)
                {
                    Message.error(this.FindForm(), "Không thể lấy dữ liệu nhiệm vụ được chọn!");
                    return;
                }
                var modalConfig = Modal.config(
                    this.FindForm(),
                    "Xác nhận xoá",
                    $"Bạn có chắc muốn xoá nhiệm vụ \"{record.TaskName}\" không?",
                    TType.Warn
                );
                modalConfig.OkText = "Xoá";
                modalConfig.CancelText = "Huỷ";
                modalConfig.OkType = TTypeMini.Success;
                modalConfig.OnOk = (cfg) =>
                {
                    try
                    {
                        _taskRepository.Delete(record.TaskID);
                        LoadData();
                        Message.success(this.FindForm(), "Xóa nhiệm vụ thành công!");
                    }
                    catch (Exception ex)
                    {
                        Message.error(this.FindForm(), "Lỗi khi xoá nhiệm vụ: " + ex.Message);
                    }
                    return true; // Đóng modal
                };
                Modal.open(modalConfig);
            }
            else
            {
                Message.error(this.FindForm(), "Không thể lấy dữ liệu nhiệm vụ được chọn!");
            }
        }
        #endregion
        #region Search / Filters / Sync
        private void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                string q = txtTim.Text?.Trim() ?? "";
                var tasks = _cachedTasks.AsEnumerable();
                if (!string.IsNullOrWhiteSpace(q))
                {
                    tasks = tasks.Where(t =>
                        !string.IsNullOrEmpty(t.TaskName) &&
                        t.TaskName.IndexOf(q, StringComparison.OrdinalIgnoreCase) >= 0);
                }
                var result = tasks.ToList();
                tbTask.DataSource = result;
                if (result.Count == 0)
                    Message.warn(this.FindForm(), "Không tìm thấy công việc phù hợp.");
                else
                {
                    Message.success(this.FindForm(), $"Tìm thấy {result.Count} nhiệm vụ.");
                }
            }
            catch (Exception ex)
            {
                Message.error(this.FindForm(), "Lỗi khi tìm kiếm: " + ex.Message);
            }
        }
        //private void ddownEmployee_SelectedValueChanged(object sender, ObjectNEventArgs e)
        //{
        // ddownStatus.Text = e.Value?.ToString() ?? "";
        // var filtered = _cachedTasks.Where(t => (t.AssignedTo ?? 0).ToString().Contains(ddownStatus.Text)).ToList();
        // tbTask.DataSource = filtered;
        //}
        //private void ddownStatus_SelectedValueChanged(object sender, ObjectNEventArgs e)
        //{
        // ddownStatus.Text = e.Value?.ToString() ?? "";
        // var filtered = _cachedTasks.Where(t => t.Status != null && t.Status.Contains(ddownStatus.Text)).ToList();
        // tbTask.DataSource = filtered;
        //}
        private TaskRepository taskRepository = new TaskRepository();
        private void ddownEmployee_SelectedValueChanged(object sender, EventArgs e)
        {
            tbTask.DataSource = null;

            string selected = ddownEmployee.SelectedValue?.ToString() ?? "";
            try
            {
                if (string.IsNullOrEmpty(selected) || selected.Equals("Tất cả", StringComparison.OrdinalIgnoreCase))
                {
                    ddownEmployee.Text = "Tất cả";
                    LoadData();
                }
                else
                {
                    var parts = selected.Split(new[] { '-' }, 2, StringSplitOptions.RemoveEmptyEntries);
                    if (parts.Length == 2 && int.TryParse(parts[0].Trim(), out int employeeId))
                    {
                        string fullName = parts[1].Trim();
                        ddownEmployee.Text = fullName;
                        var tasks = taskRepository.GetByEmployee(employeeId);
                        tbTask.DataSource = tasks.ToList();
                        Message.success(this.FindForm(), $"Tìm thấy {tasks.Count} nhiệm vụ.");
                    }
                    else
                    {
                        MessageBox.Show("Không nhận diện được nhân viên được chọn.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải nhiệm vụ: " + ex.Message);
            }
        }

        private void ddownStatus_SelectedValueChanged(object sender, ObjectNEventArgs e)
        {
            tbTask.DataSource = null;
            string selected = ddownStatus.SelectedValue.ToString();
            try
            {
                if (selected == "Tất cả" || string.IsNullOrEmpty(selected))
                {
                    LoadData();
                }
                else if (selected == "Quá hạn")
                {
                    List<Task> tasks = _cachedTasks.Where(t => t.Deadline < DateTime.Now && t.Status != "Completed").ToList();
                    tbTask.DataSource = tasks.ToList();
                }
                else
                {
                    List<Task> tasksByStatus = _cachedTasks.Where(t => t.Status == selected).ToList();
                    tbTask.DataSource = tasksByStatus.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải nhiệm vụ: " + ex.Message);
            }
        }
        private void btnSync_Click(object sender, EventArgs e)
        {
            LoadData();
            ddownEmployee.Text = "Nhân viên";
            ddownStatus.Text = "Trạng thái";
            txtTim.Text = "";
        }
        #endregion
        private void menuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "Thêm nhân viên")
            {
                var selectedIndex = tbTask.SelectedIndex - 1;
                if (selectedIndex >= 0 && tbTask.DataSource is List<Task> datalist)
                {
                    var task = datalist[selectedIndex];
                    frmAssignEmployee frmAssignEmployee = new frmAssignEmployee(task.TaskID);
                    frmAssignEmployee.ShowDialog();
                    //Console.WriteLine($"Clicked on Task ID: {task.TaskID} - {task.TaskName}");
                }
            }
            else if (e.ClickedItem.Text == "Cập nhật")
            {
                var selectedIndex = tbTask.SelectedIndex - 1;
                if (selectedIndex >= 0 && tbTask.DataSource is List<Task> datalist)
                {
                    var task = datalist[selectedIndex];
                    frmTask frmTask = new frmTask(task);
                    frmTask.ShowDialog();
                    //Console.WriteLine($"Clicked on Task ID: {task.TaskID} - {task.TaskName}");
                }
            }
        }
        private void tbTask_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int sel = tbTask.SelectedIndex;
                if (sel >= 0)
                {
                    menuStrip.Show(Cursor.Position);
                }
            }
        }

        private Table.CellStyleInfo tbTask_SetRowStyle(object sender, TableSetRowStyleEventArgs e)
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