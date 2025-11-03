using AntdUI;
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
using Message = AntdUI.Message;
using Task = EmployeeManagement.Models.Task;

namespace EmployeeManagement.Pages
{
    public partial class Page_ManageTask : UserControl
    {
        public TaskRepository _taskRepository = new TaskRepository();
        public int id;

        public Page_ManageTask()
        {
            InitializeComponent();
        }

        public void Page_ManageTask_Load(int id)
        {
            this.id = id;
            tbTask.Columns.Add(new Column("TaskID", "ID"));
            tbTask.Columns.Add(new Column("ProjectID", "Project ID"));
            tbTask.Columns.Add(new Column("TaskName", "Task Name"));
            tbTask.Columns.Add(new Column("Description", "Description"));
            tbTask.Columns.Add(new Column("AssignedTo", "Assigned To"));
            tbTask.Columns.Add(new Column("CreateBy", "CreateBy"));
            tbTask.Columns.Add(new Column("Deadline", "Deadline"));
            tbTask.Columns.Add(new Column("Status", "Status"));
            tbTask.Columns.Add(new Column("Priority", "Priority"));
            tbTask.Columns.Add(new Column("Progress", "Progress"));
            tbTask.Columns.Add(new Column("CreatedDate", "Created Date"));
            tbTask.Columns.Add(new Column("UpdateDate", "Update Date"));

            tbTask.DataSource = _taskRepository.GetByProject(this.id);
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            var selectedIndex = tbTask.SelectedIndex - 1;

            if (selectedIndex < 0)
            {
                Message.warn(this.FindForm(), "Vui lòng chọn dự án cần xóa!");
                return;
            }

            if (tbTask.DataSource is IList<Task> tasks && selectedIndex < tasks.Count)
            {
                var record = tasks[selectedIndex];

                if (record == null)
                {
                    Message.error(this.FindForm(), "Không thể lấy dữ liệu dự án được chọn!");
                    return;
                }

                var modalConfig = Modal.config(
                    this.FindForm(),
                    "Xác nhận xoá",
                    $"Bạn có chắc muốn xoá dự án \"{record.TaskName}\" không?",
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

                        Message.success(this.FindForm(), "Xóa dự án thành công!");
                    }
                    catch (Exception ex)
                    {
                        Message.error(this.FindForm(), "Lỗi khi xoá dự án: " + ex.Message);
                    }
                    return true; // Đóng modal
                };
                Modal.open(modalConfig);
            }
            else
            {
                Message.error(this.FindForm(), "Không thể lấy dữ liệu dự án được chọn!");
            }
        }

        private void LoadData()
        {
            tbTask.DataSource = _taskRepository.GetByProject(this.id);
        }
    }
}
