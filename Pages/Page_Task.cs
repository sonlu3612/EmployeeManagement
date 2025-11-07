using AntdUI;
using EmployeeManagement.DAL.Repositories;
using EmployeeManagement.Dialogs;
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

// TODO: Nếu có thể thêm progress
namespace EmployeeManagement.Pages
{
    public partial class Page_Task : UserControl
    {
        public Page_Task()
        {
            InitializeComponent();
        }

        private TaskRepository taskRepository = new TaskRepository();
        private TaskFileRepository _taskFileRepository = new TaskFileRepository();

        private void Page_Task_Load(object sender, EventArgs e)
        {
            if (IsInDesignMode()) return;
            tableTask.Columns.Add(new Column("ProjectName", "Dự án"));
            tableTask.Columns.Add(new Column("TaskName", "Nhiệm vụ"));
            tableTask.Columns.Add(new Column("EmployeeName", "Tạo bởi"));

            var docColumn = new Column("Document", "Tài liệu");
            docColumn.SetStyle(new AntdUI.Table.CellStyleInfo
            {
                ForeColor = System.Drawing.Color.Blue
            });
            tableTask.Columns.Add(docColumn);
            tableTask.Columns.Add(new Column("FileCount", "Số lượng tài liệu"));
            tableTask.Columns.Add(new Column("CreatedDate", "Ngày tạo"));
            tableTask.Columns.Add(new Column("Deadline", "Hạn hoàn thành"));
            tableTask.Columns.Add(new Column("Status", "Trạng thái"));
            //tableTask.Columns.Add(new Column("Progress", "Tiến triển"));
            tableTask.Columns.Add(new Column("Priority", "Độ ưu tiên"));

            loadData();
            loadEmployeesName();
        }

        private bool IsInDesignMode()
        {
            return LicenseManager.UsageMode == LicenseUsageMode.Designtime
                   || (this.Site != null && this.Site.DesignMode);
        }

        private List<dynamic> GetTasksWithFileCount(IEnumerable<MyTask> tasks)
        {
            return tasks.Select(t => new
            {
                t.TaskID,
                t.TaskName,
                t.ProjectName,
                t.EmployeeName,
                t.CreatedDate,
                t.Deadline,
                t.Status,
                //t.Progress,
                t.Priority,
                Document = "Mở tập tin",
                FileCount = GetTaskFileCount(t.TaskID)
            }).ToList<dynamic>();
        }

        private int GetTaskFileCount(int taskId)
        {
            try
            {
                return _taskFileRepository.GetByTask(taskId).Count;
            }
            catch
            {
                return 0;
            }
        }

        private void loadData()
        {
            tableTask.DataSource = null;
            try
            {
                var tasks = taskRepository.GetAll();

                var tasksWithFileCount = GetTasksWithFileCount(tasks);

                tableTask.DataSource = tasksWithFileCount;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading tasks: " + ex.Message);
            }
        }

        private void tableTask_CellClick(object sender, TableClickEventArgs e)
        {
            if (e.Column.Key == "Document")
            {
                if (e.Record != null)
                {
                    dynamic record = e.Record;
                    OpenTaskFileDialog(record.TaskID, record.TaskName);
                }
            }
            else
            {
                menuStrip.Show(tableTask, e.Location);
            }
        }

        private void OpenTaskFileDialog(int taskId, string taskName)
        {
            frmTaskFile frm = new frmTaskFile();
            frm.LoadTaskFiles(taskId, taskName);
            frm.ShowDialog();
            loadData();
        }

        private void btnSync_Click(object sender, EventArgs e)
        {
            loadData();
        }

        private EmployeeRepository employeeRepository = new EmployeeRepository();

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
                    loadData();
                }
                else
                {
                    List<MyTask> tasks = new List<MyTask>();

                    int employeeId = int.Parse(selected.Split('-')[0].Trim());
                    tasks = taskRepository.GetByEmployee(employeeId);

                    var tasksWithFileCount = GetTasksWithFileCount(tasks);
                    tableTask.DataSource = tasksWithFileCount;
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
            //string selected = ddownStatus.SelectedValue.ToString();
            //try
            //{
            //    if (selected == "Tất cả" || string.IsNullOrEmpty(selected))
            //    {
            //        loadData();
            //    }
            //    else
            //    {
            //        List<MyTask> tasks = new List<MyTask>();
            //        tasks = taskRepository.GetByStatus(selected);
            //    }
            //}
            //catch (Exception ex)
            //{
            //    MessageBox.Show("Lỗi tải nhiệm vụ: " + ex.Message);
            //}
        }

        private void menuStrip_ItemClicked(object sender, ToolStripItemClickedEventArgs e)
        {
            if (e.ClickedItem.Text == "Cập nhật")
            {
                var selectedIndex = tableTask.SelectedIndex - 1;
                if (selectedIndex >= 0)
                {
                    dynamic record = null;

                    if (tableTask.DataSource is IList<dynamic> dataList && selectedIndex < dataList.Count)
                    {
                        record = dataList[selectedIndex];
                    }

                    if (record != null)
                    {
                        var task = taskRepository.GetById(record.TaskID);
                        if (task != null)
                        {
                            frmTask frmTask = new frmTask(task);
                            frmTask.Show();
                        }
                    }
                }
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

            dynamic record = null;
            if (tableTask.DataSource is IList<dynamic> dataList && selectedIndex < dataList.Count)
            {
                record = dataList[selectedIndex];
            }

            if (record == null)
            {
                Message.error(this.FindForm(), "Không thể lấy dữ liệu công việc được chọn!");
                return;
            }

            var modalConfig = Modal.config(
                this.FindForm(),
                "Xác nhận xoá",
                $"Bạn có chắc muốn xoá công việc \"{record.TaskName}\" không?",
                TType.Warn
            );

            modalConfig.OkText = "Xoá";
            modalConfig.CancelText = "Huỷ";
            modalConfig.OkType = TTypeMini.Success;

            modalConfig.OnOk = (cfg) =>
            {
                try
                {
                    TaskRepository taskRepository = new TaskRepository();
                    taskRepository.Delete(record.TaskID);
                    loadData();

                    Message.success(this.FindForm(), "Xóa công việc thành công!");
                }
                catch (Exception ex)
                {
                    Message.error(this.FindForm(), "Lỗi khi xoá công việc: " + ex.Message);
                }

                return true;
            };

            Modal.open(modalConfig);
        }
    }
}