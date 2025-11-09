using AntdUI;
using EmployeeManagement.DAL.Helpers;
using EmployeeManagement.DAL.Repositories;
using EmployeeManagement.Dialogs;
using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows.Forms;
using Message = AntdUI.Message;
using MyTask = EmployeeManagement.Models.Task;

namespace EmployeeManagement.Pages
{
    public partial class Page_Task : UserControl
    {
        private readonly TaskRepository taskRepository = new TaskRepository();
        private readonly EmployeeRepository employeeRepository = new EmployeeRepository();
        private readonly ProjectRepository projectRepository = new ProjectRepository();

        // Danh sách gốc (dùng cho xóa, cập nhật, lọc)
        private List<MyTask> visibleTasks = new List<MyTask>();

        public Page_Task()
        {
            InitializeComponent();
        }

        private bool IsInDesignMode() =>
            LicenseManager.UsageMode == LicenseUsageMode.Designtime ||
            (Site != null && Site.DesignMode);

        private bool IsAdmin() =>
            SessionManager.CurrentUser?.Roles?.Contains("Admin") ?? false;

        private bool IsProjectManager() =>
            SessionManager.CurrentUser?.Roles?.Contains("Quản lý dự án") ?? false;

        private bool IsEmployee() =>
            SessionManager.CurrentUser?.Roles?.Contains("Nhân viên") ?? false;

        // Hàm tạo danh sách hiển thị (chỉ ngày)
        private object CreateDisplayItem(MyTask t)
        {
            var stats = taskRepository.GetTaskAssignmentStats(t.TaskID);
            int progressValue = stats.Item3 > 0 ? (int)stats.Item3 : 0;
            string progressText = $"{stats.Item2}/{stats.Item1}";

            var cellProgress = new AntdUI.CellProgress((float)progressValue / 100)
            {
                Size = new Size(60, 10),
            };

            return new
            {
                t.ProjectName,
                t.TaskName,
                t.EmployeeName,
                CreatedDateDisplay = t.CreatedDate.ToString("dd/MM/yyyy"),
                DeadlineDisplay = t.Deadline.HasValue
                    ? t.Deadline.Value.ToString("dd/MM/yyyy")
                    : "N/A",
                t.Status,
                t.Priority,
                Progress = cellProgress,
                ProgressText = progressText,
                t.TaskID,
                t.ProjectID
            };
        }

        public void loadData()
        {
            tableTask.DataSource = null;
            try
            {
                List<MyTask> tasks = new List<MyTask>();

                if (IsAdmin())
                {
                    tasks = taskRepository.GetAll().ToList();
                }
                else if (IsProjectManager())
                {
                    var myProjectIds = projectRepository.GetAll()
                        .Where(p => p.ManagerBy == SessionManager.CurrentUser.UserID)
                        .Select(p => p.ProjectID)
                        .ToList();

                    foreach (var id in myProjectIds)
                    {
                        tasks.AddRange(taskRepository.GetByProject(id)); // GỘP TẤT CẢ
                    }
                }
                else if (IsEmployee())
                {
                    tasks = taskRepository.GetByEmployee(SessionManager.CurrentUser.UserID);
                }

                visibleTasks = tasks; // Lưu lại

                var displayList = tasks.Select(t => CreateDisplayItem(t)).ToList();
                tableTask.DataSource = displayList;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải nhiệm vụ: " + ex.Message);
            }
        }

        private void btnSync_Click(object sender, EventArgs e) => loadData();

        private void loadEmployeesName()
        {
            var employees = employeeRepository.GetAll();
            ddownEmployee.Items.Clear();
            ddownEmployee.Items.Insert(0, "Tất cả");
            foreach (var emp in employees)
                ddownEmployee.Items.Add($"{emp.EmployeeID} - {emp.FullName}");
        }

        private void ddownEmployee_SelectedValueChanged(object sender, EventArgs e)
        {
            tableTask.DataSource = null;
            string selected = ddownEmployee.SelectedValue?.ToString() ?? "";
            ddownEmployee.Text = selected;

            try
            {
                if (string.IsNullOrWhiteSpace(selected) ||
                    selected.Equals("Tất cả", StringComparison.OrdinalIgnoreCase))
                {
                    var displayList = visibleTasks.Select(t => CreateDisplayItem(t)).ToList();
                    tableTask.DataSource = displayList;
                    Message.success(this.FindForm(), $"Tìm thấy {visibleTasks.Count} nhiệm vụ.");
                    return;
                }

                var parts = selected.Split(new[] { '-' }, 2, StringSplitOptions.RemoveEmptyEntries);
                
                string fullName = parts[1].Trim();
                ddownEmployee.Text = fullName;
                
                if (parts.Length == 2 && int.TryParse(parts[0].Trim(), out int employeeId))
                {
                    var tasks = taskRepository.GetByEmployee(employeeId);
                    var display = tasks.Select(t => CreateDisplayItem(t)).ToList();
                    tableTask.DataSource = display;
                }
                else
                {
                    Message.warn(this.FindForm(), "Không thể nhận diện nhân viên được chọn.");
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
            string selected = ddownStatus.SelectedValue?.ToString() ?? "";
            ddownStatus.Text = selected;

            try
            {
                if (selected == "Tất cả" || string.IsNullOrEmpty(selected))
                {
                    var displayList = visibleTasks.Select(t => CreateDisplayItem(t)).ToList();
                    tableTask.DataSource = displayList;
                    return;
                }

                IEnumerable<MyTask> filtered;

                if (selected == "Hết hạn")
                {
                    filtered = taskRepository.GetOverdueTasks()
                        .Where(t => visibleTasks.Any(v => v.TaskID == t.TaskID));
                }
                else
                {
                    filtered = visibleTasks.Where(t => t.Status == selected);
                }

                var display = filtered.Select(t => CreateDisplayItem(t)).ToList();
                tableTask.DataSource = display;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi tải nhiệm vụ: " + ex.Message);
            }
        }

        // Lấy task từ hàng được chọn (DataSource là anonymous)
        private MyTask GetSelectedTask()
        {
            int index = tableTask.SelectedIndex-1;
            if (index < 0) return null;

            if (tableTask.DataSource is System.Collections.IList list && index < list.Count)
            {
                dynamic row = list[index];
                int taskId = row.TaskID;
                return visibleTasks.FirstOrDefault(t => t.TaskID == taskId);
            }
            return null;
        }

        private void menuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            var task = GetSelectedTask();
            if (task == null) return;

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
                var frm = new frmTask(task);
                frm.ShowDialog();
            }
            else if (e.ClickedItem.Text == "Danh sách nhân viên")
            {
                if (!IsAdmin() && !isProjectManagerOfThis)
                {
                    Message.warn(this.FindForm(), "Bạn không có quyền quản lý nhân viên cho nhiệm vụ này!");
                    return;
                }
                new frmAssignEmployee(task.TaskID).ShowDialog();
            }
            else if (e.ClickedItem.Text == "Cập nhật tiến độ")
            {
                bool isAssigned = task.AssignedEmployeeIds != null && task.AssignedEmployeeIds.Contains(userId);
                if (!isAssigned)
                {
                    Message.warn(this.FindForm(), "Bạn không có quyền cập nhật tiến độ cho nhiệm vụ này!");
                    return;
                }
                if (task.Deadline.HasValue && DateTime.Now.Date > task.Deadline.Value.Date)
                {
                    if (!IsAdmin() && !IsProjectManager())
                    {
                        Message.warn(this.FindForm(), "Nhiệm vụ đã hết hạn, bạn chỉ có thể xem!");
                        return;
                    }
                }
                new frmSubmit(task, userId).ShowDialog();
            }
            else if (e.ClickedItem.Text == "Tài liệu")
            {
                if (!IsAdmin() && !isProjectManagerOfThis)
                {
                    Message.warn(this.FindForm(), "Bạn không có quyền xem tài liệu cho nhiệm vụ này!");
                    return;
                }
                var frmFile = new frmTaskFile(task.TaskID);
                frmFile.ShowDialog();
            }
            loadData();
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var task = GetSelectedTask();
            if (task == null)
            {
                Message.warn(this.FindForm(), "Vui lòng chọn công việc cần xóa!");
                return;
            }

            var userId = SessionManager.CurrentUser.UserID;
            var project = projectRepository.GetById(task.ProjectID);
            if (!IsAdmin() && !(IsProjectManager() && project?.ManagerBy == userId))
            {
                Message.warn(this.FindForm(), "Bạn không có quyền xóa nhiệm vụ này!");
                return;
            }

            var cfg = Modal.config(this.FindForm(),
                "Xác nhận xoá",
                $"Bạn có chắc muốn xoá nhiệm vụ \"{task.TaskName}\" không?",
                TType.Warn);
            cfg.OkText = "Xoá";
            cfg.CancelText = "Huỷ";
            cfg.OkType = TTypeMini.Primary;
            cfg.OnOk = _ =>
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
                return true;
            };
            Modal.open(cfg);
        }

        private void menuStrip_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right && tableTask.SelectedIndex >= 0)
                menuStrip.Show(Cursor.Position);
        }

        private void Page_Task_Load(object sender, EventArgs e)
        {
            if (IsInDesignMode()) return;

            tableTask.Columns.Clear();
            tableTask.Columns.Add(new Column("ProjectName", "Dự án"));
            tableTask.Columns.Add(new Column("TaskName", "Nhiệm vụ"));
            tableTask.Columns.Add(new Column("EmployeeName", "Tạo bởi"));
            tableTask.Columns.Add(new Column("CreatedDateDisplay", "Ngày tạo"));
            tableTask.Columns.Add(new Column("DeadlineDisplay", "Hạn hoàn thành"));
            tableTask.Columns.Add(new Column("Status", "Trạng thái"));
            tableTask.Columns.Add(new Column("Priority", "Độ ưu tiên"));
            tableTask.Columns.Add(new Column("Progress", "Tiến độ"));
            tableTask.Columns.Add(new Column("ProgressText", "Tiến độ (Chữ)"));

            loadData();
            loadEmployeesName();

            if (!IsAdmin() && !IsProjectManager())
                btnDelete.Enabled = false;
        }

        private Table.CellStyleInfo tableTask_SetRowStyle(object sender, TableSetRowStyleEventArgs e)
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