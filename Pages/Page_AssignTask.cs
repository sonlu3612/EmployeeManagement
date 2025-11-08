using AntdUI;
using EmployeeManagement.DAL.Helpers;
using EmployeeManagement.DAL.Repositories;
using EmployeeManagement.DAL.Services;
using EmployeeManagement.Models;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using Message = AntdUI.Message;
using Task = EmployeeManagement.Models.Task;

namespace EmployeeManagement.Pages
{
    public partial class Page_AssignTask : UserControl
    {
        public Page_AssignTask()
        {
            InitializeComponent();
        }

        private TaskRepository repository = new TaskRepository();
        private int EmployeeID;
        private List<Task> originalTasks; // Lưu danh sách gốc

        public void Page_AssignTask_Load(int id)
        {
            this.EmployeeID = id;
            tbTask.Columns.Add(new Column("ProjectName", "Tên dự án"));
            tbTask.Columns.Add(new Column("TaskName", "Tên nhiệm vụ"));
            tbTask.Columns.Add(new Column("Description", "Mô tả"));
            tbTask.Columns.Add(new Column("Deadline", "Đến hạn"));
            tbTask.Columns.Add(new Column("Status", "Trạng thái"));
            tbTask.Columns.Add(new Column("Priority", "Độ ưu tiên"));
            tbTask.Columns.Add(new Column("Progress", "Tiến triển"));
            LoadData();
        }

        private void LoadData()
        {
            try
            {
                originalTasks = repository.GetByEmployee(this.EmployeeID);
                tbTask.DataSource = originalTasks;
                ddownStatus.Items.Add("Tất cả");
                ddownStatus.Items.Add("Cần làm");
                ddownStatus.Items.Add("Đang thực hiện");
                ddownStatus.Items.Add("Hoàn thành");
            }
            catch (Exception ex)
            {
                Message.error(this.FindForm(), "Lỗi khi tải dữ liệu: " + ex.Message);
            }
        }

        private void btnSync_Click(object sender, EventArgs e)
        {
            txtTim.Text = string.Empty;
            ddownStatus.Text = "Trạng thái";
            LoadData();
        }

        private void ddownStatus_SelectedValueChanged(object sender, ObjectNEventArgs e)
        {
            string selectedStatus = e.Value?.ToString() ?? "";
            ddownStatus.Text = selectedStatus;
            try
            {
                if (string.IsNullOrEmpty(selectedStatus) || selectedStatus == "Tất cả")
                {
                    tbTask.DataSource = originalTasks;
                    return;
                }

                var filtered = originalTasks.Where(t => t.Status == selectedStatus).ToList();
                tbTask.DataSource = filtered;
                if (filtered.Count == 0)
                {
                    Message.warn(this.FindForm(), "Không tìm thấy nhiệm vụ với trạng thái này.");
                }
            }
            catch (Exception ex)
            {
                Message.error(this.FindForm(), "Lỗi khi lọc theo trạng thái: " + ex.Message);
            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            string searchText = txtTim.Text?.Trim().ToLower() ?? "";
            try
            {
                if (string.IsNullOrEmpty(searchText))
                {
                    tbTask.DataSource = originalTasks;
                    return;
                }

                var filtered = originalTasks.Where(t =>
                    (t.TaskName?.ToLower().Contains(searchText) ?? false) ||
                    (t.Description?.ToLower().Contains(searchText) ?? false)
                ).ToList();

                tbTask.DataSource = filtered;
                if (filtered.Count == 0)
                {
                    Message.warn(this.FindForm(), "Không tìm thấy kết quả phù hợp.");
                }
                else
                {
                    Message.success(this.FindForm(), $"Tìm thấy {filtered.Count} nhiệm vụ.");
                }
            }
            catch (Exception ex)
            {
                Message.error(this.FindForm(), "Lỗi khi tìm kiếm: " + ex.Message);
            }
        }
    }
}