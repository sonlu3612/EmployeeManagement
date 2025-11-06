using AntdUI;
using EmployeeManagement.DAL.Helpers;
using EmployeeManagement.DAL.Repositories;
using EmployeeManagement.Dialogs;
using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows.Forms;
using Message = AntdUI.Message;
using MyTask = EmployeeManagement.Models.Task;
using Task = EmployeeManagement.Models.Task;
namespace EmployeeManagement.Pages
{
    public partial class Page_Task : UserControl
    {
        private TaskRepository taskRepository = new TaskRepository();
        private EmployeeRepository employeeRepository = new EmployeeRepository();
        private ProjectRepository projectRepository = new ProjectRepository();
        private List<MyTask> visibleTasks;
        public Page_Task()
        {
            InitializeComponent();
        }
        private bool IsInDesignMode()
        {
            return LicenseManager.UsageMode == LicenseUsageMode.Designtime
                   || (this.Site != null && this.Site.DesignMode);
        }
        private bool IsAdmin()
        {
            return SessionManager.CurrentUser?.Roles?.Contains("Admin") ?? false;
        }
        private bool IsProjectManager()
        {
            return SessionManager.CurrentUser?.Roles?.Contains("Quản lý dự án") ?? false;
        }
        private bool IsEmployee()
        {
            return SessionManager.CurrentUser?.Roles?.Contains("Nhân viên") ?? false;
        }
        private void loadData()
        {
            tableTask.DataSource = null;
            try
            {
                if (IsAdmin())
                {
                    visibleTasks = taskRepository.GetAll().ToList();
                }
                else if (IsProjectManager())
                {
                    var myProjectIds = projectRepository.GetAll()
                        .Where(p => p.ManagerBy == SessionManager.CurrentUser.UserID)
                        .Select(p => p.ProjectID)
                        .ToList();
                    foreach (var id in myProjectIds)
                    {
                        visibleTasks = taskRepository.GetByProject(id);
                    }
                }
                else if (IsEmployee())
                {
                    visibleTasks = taskRepository.GetByEmployee(SessionManager.CurrentUser.UserID);
                }
                else
                {
                    visibleTasks = new List<MyTask>();
                }
                tableTask.DataSource = visibleTasks;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading tasks: " + ex.Message);
            }
        }
        private void tableTask_CellClick(object sender, TableClickEventArgs e)
        {
        }
        private void btnSync_Click(object sender, EventArgs e)
        {
            loadData();
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
        private void ddownEmployee_SelectedValueChanged(object sender, EventArgs e)
        {
            tableTask.DataSource = null;
            string selected = ddownEmployee.SelectedValue.ToString();
            try
            {
                if (selected == "Tất cả" || string.IsNullOrEmpty(selected))
                {
                    tableTask.DataSource = visibleTasks;
                }
                else
                {
                    int employeeId = int.Parse(selected.Split('-')[0].Trim());
                    List<Task> tasks;
                    tasks = taskRepository.GetByEmployee(employeeId);
                    tableTask.DataSource = tasks;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải nhiệm vụ: " + ex.Message);
            }
        }
        private void ddownStatus_SelectedValueChanged(object sender, ObjectNEventArgs e)
        {
            tableTask.DataSource = null;
            string selected = ddownStatus.SelectedValue.ToString();
            try
            {
                if (selected == "Tất cả" || string.IsNullOrEmpty(selected))
                {
                    tableTask.DataSource = visibleTasks;
                }
                else if (selected == "Quá hạn")
                {
                    List<MyTask> tasks = taskRepository.GetOverdueTasks()
                        .Where(t => visibleTasks.Select(v => v.TaskID).Contains(t.TaskID))
                        .ToList();
                    tableTask.DataSource = tasks.ToList();
                }
                else
                {
                    List<MyTask> tasksByStatus = visibleTasks
                        .Where(t => t.Status == selected)
                        .ToList();
                    tableTask.DataSource = tasksByStatus.ToList();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải nhiệm vụ: " + ex.Message);
            }
        }
        private void menuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            var selectedIndex = tableTask.SelectedIndex - 1;
            if (selectedIndex < 0 || !(tableTask.DataSource is List<MyTask> datalist))
            {
                return;
            }
            var task = datalist[selectedIndex];
            var userId = SessionManager.CurrentUser.UserID;
            var project = projectRepository.GetById(task.ProjectID);
            bool isProjectManagerOfThis = IsProjectManager() && project?.ManagerBy == userId;
            if (e.ClickedItem.Text == "Cập nhật")
            {
                if (!IsAdmin() && !isProjectManagerOfThis)
                {
                    Message.warn(this.FindForm(), "Bạn không có quyền cập nhật nhiệm vụ này!");
                    return;
                }
                frmTask frmTask = new frmTask(task);
                frmTask.ShowDialog();
            }
            else if (e.ClickedItem.Text == "Danh sách nhân viên")
            {
                if (!IsAdmin() && !isProjectManagerOfThis)
                {
                    Message.warn(this.FindForm(), "Bạn không có quyền quản lý nhân viên cho nhiệm vụ này!");
                    return;
                }
                frmAssignEmployee frmAssignEmployee = new frmAssignEmployee(task.TaskID);
                frmAssignEmployee.ShowDialog();
            }
            else if (e.ClickedItem.Text == "Cập nhật tiến độ")
            {
                bool isAssignedToTask = task.AssignedEmployeeIds != null && task.AssignedEmployeeIds.Contains(userId);

                if (!isAssignedToTask)
                {
                    Message.warn(this.FindForm(), "Bạn không có quyền cập nhật tiến độ cho nhiệm vụ này!");
                    return;
                }

                frmSubmit frm = new frmSubmit(task, userId);
                frm.ShowDialog();
            }
        }
        private void btnDelete_Click(object sender, EventArgs e)
        {
            var selectedIndex = tableTask.SelectedIndex - 1;
            if (selectedIndex < 0)
            {
                Message.warn(this.FindForm(), "Vui lòng chọn công việc cần xóa!");
                return;
            }
            if (tableTask.DataSource is IList<MyTask> list && selectedIndex < list.Count)
            {
                var task = list[selectedIndex];
                var userId = SessionManager.CurrentUser.UserID;
                var project = projectRepository.GetById(task.ProjectID);
                if (!IsAdmin() && !(IsProjectManager() && project?.ManagerBy == userId))
                {
                    Message.warn(this.FindForm(), "Bạn không có quyền xóa nhiệm vụ này!");
                    return;
                }
                var modalConfig = Modal.config(
                    this.FindForm(),
                    "Xác nhận xoá",
                    $"Bạn có chắc muốn xoá nhiệm vụ \"{task.TaskName}\" không?",
                    TType.Warn
                );
                modalConfig.OkText = "Xoá";
                modalConfig.CancelText = "Huỷ";
                modalConfig.OkType = TTypeMini.Primary;
                modalConfig.OnOk = (cfg) =>
                {
                    try
                    {
                        taskRepository.Delete(task.TaskID);
                        loadData();
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
        private void menuStrip_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                int sel = tableTask.SelectedIndex;
                if (sel >= 0)
                {
                    menuStrip.Show(Cursor.Position);
                }
            }
        }
        private void taskToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int selectedIndex = tableTask.SelectedIndex - 1;
            if (tableTask.DataSource is IList<EmployeeManagement.Models.Task> tasks && selectedIndex >= 0 && selectedIndex < tasks.Count)
            {
                var record = tasks[selectedIndex];
                if (record == null) { Message.error(this.FindForm(), "Không thể lấy dữ liệu nhiệm vụ được chọn!"); return; }
                int id = record.ProjectID;
                ManageEmployee frm = new ManageEmployee();
                //frm.frmManageTasks_Load(id);
                frm.ShowDialog();
            }
            else
            {
                Message.error(this.FindForm(), "Không thể lấy dữ liệu nhiệm vụ được chọn!");
            }
        }
        private void cậpNhậtTiếnĐộToolStripMenuItem_Click(object sender, EventArgs e)
        {
        }
        private void Page_Task_Load(object sender, EventArgs e)
        {
            if (IsInDesignMode()) return;
            tableTask.Columns.Add(new Column("ProjectName", "Dự án"));
            tableTask.Columns.Add(new Column("TaskName", "Nhiệm vụ"));
            tableTask.Columns.Add(new Column("EmployeeName", "Tạo bởi"));
            tableTask.Columns.Add(new Column("CreatedDate", "Ngày tạo"));
            tableTask.Columns.Add(new Column("Deadline", "Hạn hoàn thành"));
            tableTask.Columns.Add(new Column("Status", "Trạng thái"));
            tableTask.Columns.Add(new Column("Progress", "Tiến triển"));
            tableTask.Columns.Add(new Column("Priority", "Độ ưu tiên"));
            loadData();
            loadEmployeesName();
            if (!IsAdmin() && !IsProjectManager())
            {
                btnDelete.Enabled = false;
            }
        }
    }
}