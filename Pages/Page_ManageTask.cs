using AntdUI;
using EmployeeManagement.DAL.Repositories;
using EmployeeManagement.Dialogs;
using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Message = AntdUI.Message;
using Task = EmployeeManagement.Models.Task;

namespace EmployeeManagement.Pages
{
    public partial class Page_ManageTask : UserControl
    {
        private TaskRepository _taskRepository = new TaskRepository();
        private int projectId;
        private List<Task> _cachedTasks = new List<Task>();

        public Page_ManageTask()
        {
            InitializeComponent();
        }

        public void Page_ManageTask_Load(int id)
        {
            this.projectId = id;

            tbTask.Columns.Clear();
            tbTask.Columns.Add(new Column("TaskID", "ID"));
            tbTask.Columns.Add(new Column("ProjectName", "Project Name"));
            tbTask.Columns.Add(new Column("TaskName", "Task Name"));
            tbTask.Columns.Add(new Column("Description", "Description"));
            tbTask.Columns.Add(new Column("CreateBy", "Owner"));
            tbTask.Columns.Add(new Column("Deadline", "Deadline"));
            tbTask.Columns.Add(new Column("Status", "Status"));
            tbTask.Columns.Add(new Column("Priority", "Priority"));
            tbTask.Columns.Add(new Column("Progress", "Progress"));
            tbTask.Columns.Add(new Column("CreatedDate", "Created Date"));
            tbTask.Columns.Add(new Column("UpdateDate", "Update Date"));

            LoadData();
        }

        private void LoadData()
        {
            _cachedTasks = _taskRepository.GetByProject(this.projectId).ToList();

            tbTask.DataSource = _cachedTasks;
            //if (ddownEmployee.Items.Count == 0)
            //{
            //    var assignedList = _cachedTasks
            //        .Select(t => (t.AssignedTo ?? 0).ToString()) 
            //        .Distinct()
            //        .OrderBy(x => x)
            //        .ToArray();

            //    if (assignedList.Length > 0)
            //        ddownStatus.Items.AddRange(assignedList);
            //}

            try
            {
                if (ddownStatus.Items.Count == 0)
                {
                    var statusList = _cachedTasks
                        .Select(t => t.Status ?? "")
                        .Where(s => !string.IsNullOrWhiteSpace(s))
                        .Distinct()
                        .OrderBy(x => x)
                        .ToArray();

                    if (statusList.Length > 0)
                        ddownStatus.Items.AddRange(statusList);
                }
            }
            catch
            {
                // ignore
            }
        }

        #region Add / Edit / Delete / Click

        private void btnThem_Click(object sender, EventArgs e)
        {
            //frmTask frm = new frmTask(projectId); // nếu constructor có param projectId
            //if (frm.ShowDialog() == DialogResult.OK)
            //{
            //    var newTask = frm.Tag as Task;
            //    if (newTask != null)
            //    {
            //        _taskRepository.Insert(newTask);
            //        LoadData();
            //        Message.success(this.FindForm(), "Thêm task thành công!");
            //    }
            //}
        }

        private void tbTask_Click(object sender, EventArgs e)
        {
            //var selectedIndex = tbTask.SelectedIndex;
            //if (tbTask.DataSource is List<Task> tasks && selectedIndex >= 0 && selectedIndex < tasks.Count)
            //{
            //    var task = tasks[selectedIndex];

            //    // Mở form edit (giả sử frmTask có constructor từ Task)
            //    frmTask frm = new frmTask(task);
            //    if (frm.ShowDialog() == DialogResult.OK)
            //    {
            //        var updatedTask = frm.Tag as Task;
            //        if (updatedTask != null)
            //        {
            //            _taskRepository.Update(updatedTask);
            //            LoadData();
            //            Message.success(this.FindForm(), "Cập nhật task thành công!");
            //        }
            //    }
            //}
            //else
            //{
            //    Message.warn(this.FindForm(), "Vui lòng chọn task cần sửa!");
            //}
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var selectedIndex = tbTask.SelectedIndex - 1;

            if (selectedIndex < 0)
            {
                Message.warn(this.FindForm(), "Vui lòng chọn task cần xóa!");
                return;
            }

            if (tbTask.DataSource is IList<Task> tasks && selectedIndex < tasks.Count)
            {
                var record = tasks[selectedIndex];

                if (record == null)
                {
                    Message.error(this.FindForm(), "Không thể lấy dữ liệu task được chọn!");
                    return;
                }

                var modalConfig = Modal.config(
                    this.FindForm(),
                    "Xác nhận xoá",
                    $"Bạn có chắc muốn xoá task \"{record.TaskName}\" không?",
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

                        Message.success(this.FindForm(), "Xóa task thành công!");
                    }
                    catch (Exception ex)
                    {
                        Message.error(this.FindForm(), "Lỗi khi xoá task: " + ex.Message);
                    }
                    return true; // Đóng modal
                };
                Modal.open(modalConfig);
            }
            else
            {
                Message.error(this.FindForm(), "Không thể lấy dữ liệu task được chọn!");
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

                //if (!string.IsNullOrWhiteSpace(ddownStatus.Text) && ddownStatus.Text != "Người được giao")
                //{
                //    string assFilter = ddownStatus.Text.Trim();
                //    tasks = tasks.Where(t => (t.AssignedTo ?? 0).ToString().Contains(assFilter));
                //}

                if (!string.IsNullOrWhiteSpace(ddownStatus.Text) && ddownStatus.Text != "Trạng thái")
                {
                    string statusFilter = ddownStatus.Text.Trim();
                    tasks = tasks.Where(t => !string.IsNullOrEmpty(t.Status) &&
                                             t.Status.IndexOf(statusFilter, StringComparison.OrdinalIgnoreCase) >= 0);
                }

                var result = tasks.ToList();
                tbTask.DataSource = result;

                if (result.Count == 0)
                    Message.warn(this.FindForm(), "Không tìm thấy kết quả phù hợp.");
            }
            catch (Exception ex)
            {
                Message.error(this.FindForm(), "Lỗi khi tìm kiếm: " + ex.Message);
            }
        }

        private void ddownStatus_SelectedValueChanged(object sender, ObjectNEventArgs e)
        {
            ddownStatus.Text = e.Value?.ToString() ?? "";
            var filtered = _cachedTasks.Where(t => t.Status != null && t.Status.Contains(ddownStatus.Text)).ToList();
            tbTask.DataSource = filtered;
        }

        private void btnSync_Click(object sender, EventArgs e)
        {
            LoadData();
            ddownStatus.Text = "Nhân viên";
            ddownStatus.Text = "Trạng thái";
            txtTim.Text = "";
        }

        #endregion

        private void tbTask_CellClick(object sender, TableClickEventArgs e)
        {

        }
    }
}
